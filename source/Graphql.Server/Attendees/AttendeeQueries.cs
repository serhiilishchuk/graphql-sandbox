using Graphql.Server.Data;
using Graphql.Server.DataLoader;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Graphql.Server.Attendees
{
    [ExtendObjectType("Query")]
    public class AttendeeQueries
    {
        public IQueryable<Attendee> GetAttendees([ScopedService] AppDbContext context) => context.Attendees;

        public Task<Attendee> GetAttendeeByIdAsync([ID(nameof(Attendee))] int id, AttendeeByIdDataLoader attendeeById,
            CancellationToken token) => attendeeById.LoadAsync(id, token);

        public async Task<IEnumerable<Attendee>> GetAttendeesByIdsAsync([ID(nameof(Attendee))] int[] ids,
            AttendeeByIdDataLoader attendeeById, CancellationToken token) => await attendeeById.LoadAsync(ids, token);
    }
}
