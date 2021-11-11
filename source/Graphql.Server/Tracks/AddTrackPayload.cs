using Graphql.Server.Common;
using Graphql.Server.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graphql.Server.Tracks
{
    public class AddTrackPayload : TrackPayloadBase
    {
        public AddTrackPayload(Track track)
            : base(track)
        {

        }

        public AddTrackPayload(IReadOnlyList<UserError> errors)
            : base(errors)
        {

        }
    }
}
