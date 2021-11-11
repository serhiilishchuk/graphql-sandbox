using Graphql.Server.Data;
using Graphql.Server.DataLoader;
using Graphql.Server.Extensions;
using HotChocolate;
using HotChocolate.Types.Relay;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Graphql.Server.Attendees
{
    public class SessionAttendeeCheckIn
    {
        public SessionAttendeeCheckIn(int attendeeId, int sessionId)
        {
            AttendeeId = attendeeId;
            SessionId = sessionId;
        }

        [ID(nameof(Attendee))]
        public int AttendeeId { get; }

        [ID(nameof(Session))]
        public int SessionId { get; }

        [UseApplicationDbContext]
        public async Task<int> CheckInCountAsync([ScopedService] AppDbContext context, CancellationToken cancellationToken)
            => await context.Sessions
                .Where(session => session.Id == SessionId)
                .SelectMany(session => session.SessionAttendees)
                .CountAsync(cancellationToken);

        public Task<Attendee> GetAttendeeAsync(AttendeeByIdDataLoader attendeeById, CancellationToken cancellationToken)
            => attendeeById.LoadAsync(AttendeeId, cancellationToken);

        public Task<Session> GetSessionAsync(SessionByIdDataLoader sessionById, CancellationToken cancellationToken)
            => sessionById.LoadAsync(AttendeeId, cancellationToken);
    }
}
