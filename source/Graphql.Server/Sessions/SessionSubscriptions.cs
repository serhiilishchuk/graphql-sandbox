using Graphql.Server.Data;
using Graphql.Server.DataLoader;
using HotChocolate;
using HotChocolate.Types;
using System.Threading;
using System.Threading.Tasks;

namespace Graphql.Server.Sessions
{
    [ExtendObjectType("Subscription")]
    public class SessionSubscriptions
    {
        [Subscribe]
        [Topic]
        public Task<Session> OnSessionScheduledAsync([EventMessage] int sessionId, SessionByIdDataLoader sessionById,
            CancellationToken cancellationToken)

            => sessionById.LoadAsync(sessionId, cancellationToken);
    }
}
