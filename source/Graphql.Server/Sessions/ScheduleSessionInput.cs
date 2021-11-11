using Graphql.Server.Data;
using HotChocolate.Types.Relay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graphql.Server.Sessions
{
    public record ScheduleSessionInput(
        [ID(nameof(Session))]
        int SessionId,
        [ID(nameof(Track))]
        int TrackId,
        DateTimeOffset StartTime,
        DateTimeOffset EndTime);
}
