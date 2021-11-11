using Graphql.Server.Common;
using Graphql.Server.Data;
using Graphql.Server.Extensions;
using HotChocolate;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Graphql.Server.Attendees
{
    [ExtendObjectType("Mutation")]
    public class AttendeeMutations
    {
        [UseApplicationDbContext]
        public async Task<RegisterAttendeePayload> RegisterAttendeeAsync(RegisterAttendeeInput input,
            [ScopedService] AppDbContext context, CancellationToken cancellationToken)
        {
            var attendee = new Attendee
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                UserName = input.UserName,
                EmailAddress = input.Email
            };

            context.Attendees.Add(attendee);

            await context.SaveChangesAsync(cancellationToken);

            return new RegisterAttendeePayload(attendee);
        }

        [UseApplicationDbContext]
        public async Task<CheckInAttendeePayload> CheckInAttendeeAsync(CheckInAttendeeInput input,
            [ScopedService] AppDbContext context, [Service] ITopicEventSender eventSender, CancellationToken token)
        {
            Attendee attendee = await context.Attendees
                .FirstOrDefaultAsync(t => t.Id == input.AttendeeId, token);

            if (attendee is null)
            {
                return new CheckInAttendeePayload(new UserError("Attendee not found.", "ATTENDEE_NOT_FOUND"));
            }

            attendee.SessionsAttendees.Add(new SessionAttendee
            {
                SessionId = input.SessionId
            });

            await context.SaveChangesAsync(token);

            await eventSender.SendAsync("OnAttendeeCheckedIn_" + input.SessionId, input.AttendeeId, token);

            return new CheckInAttendeePayload(attendee, input.SessionId);
        }
    }
}
