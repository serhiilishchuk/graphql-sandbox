using Graphql.Server.Data;
using Graphql.Server.DataLoader;
using Graphql.Server.Extensions;
using Graphql.Server.Types;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Graphql.Server.Sessions
{
    [ExtendObjectType("Query")]
    public class SessionQuery
    {
        [UseApplicationDbContext]
        [UsePaging(typeof(NonNullType<SessionType>))]
        [UseFiltering(typeof(SessionFilterInputType))]
        [UseSorting]
        public IQueryable<Session> GetSessions([ScopedService] AppDbContext context) =>
            context.Sessions;

        public Task<Session> GetSessionByIdAsync([ID(nameof(Session))] int id, SessionByIdDataLoader sessionById,
            CancellationToken cancellationToken) =>
            sessionById.LoadAsync(id, cancellationToken);

        public async Task<IEnumerable<Session>> GetSessionsByIdAsync([ID(nameof(Session))] int[] ids, SessionByIdDataLoader sessionById,
            CancellationToken cancellationToken) =>
            await sessionById.LoadAsync(ids, cancellationToken);
    }
}
