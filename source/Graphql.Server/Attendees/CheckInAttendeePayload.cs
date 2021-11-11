using Graphql.Server.Common;
using Graphql.Server.Data;
using Graphql.Server.DataLoader;
using System.Threading;
using System.Threading.Tasks;

namespace Graphql.Server.Attendees
{
    public class CheckInAttendeePayload : AttendeePayloadBase
    {
        private int? _sessionId;

        public CheckInAttendeePayload(Attendee attendee, int sessionId)
            : base(attendee)
        {
            _sessionId = sessionId;
        }

        public CheckInAttendeePayload(UserError error)
            : base(new[] { error })
        {

        }

        public async Task<Session?> GetSessionAsync(SessionByIdDataLoader sessionById, CancellationToken token)
        {
            if (_sessionId.HasValue)
            {
                return await sessionById.LoadAsync(_sessionId.Value, token);
            }

            return null;
        }
    }
}
