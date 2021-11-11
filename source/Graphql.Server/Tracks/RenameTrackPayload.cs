using Graphql.Server.Common;
using Graphql.Server.Data;
using System.Collections.Generic;

namespace Graphql.Server.Tracks
{
    public class RenameTrackPayload : TrackPayloadBase
    {
        public RenameTrackPayload(Track track)
            : base(track)
        {

        }

        public RenameTrackPayload(IReadOnlyList<UserError> errors)
            : base(errors)
        {

        }
    }
}
