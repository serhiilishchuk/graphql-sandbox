using Graphql.Server.Data;
using Graphql.Server.DataLoader;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using System.Threading;
using System.Threading.Tasks;

namespace Graphql.Server.Attendees
{
    [ExtendObjectType("Subscription")]
    public class AttendeeSubscriptions
    {
        [Subscribe(With = nameof(SubscribeToOnAttendeeCheckedInAsync))]
        public SessionAttendeeCheckIn OnAttendeeCheckedIn([ID(nameof(Session))] int sessionId, [EventMessage] int attendeeId,
            SessionByIdDataLoader sessionById, CancellationToken cancellationToken)
            => new SessionAttendeeCheckIn(attendeeId, sessionId);

        public async ValueTask<ISourceStream<int>> SubscribeToOnAttendeeCheckedInAsync(int sessionId,
            [Service] ITopicEventReceiver eventReceiver, CancellationToken cancellationToken)
            => await eventReceiver.SubscribeAsync<string, int>("OnAttendeeCheckedIn_" + sessionId, cancellationToken);
    }
}
