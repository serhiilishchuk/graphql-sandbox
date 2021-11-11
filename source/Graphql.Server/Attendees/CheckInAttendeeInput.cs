using Graphql.Server.Data;
using HotChocolate.Types.Relay;

namespace Graphql.Server.Attendees
{
    public record CheckInAttendeeInput([ID(nameof(Session))] int SessionId, [ID(nameof(Attendee))] int AttendeeId);
}
