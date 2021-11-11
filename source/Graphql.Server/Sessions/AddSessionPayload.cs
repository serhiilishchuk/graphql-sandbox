using Graphql.Server.Common;
using Graphql.Server.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graphql.Server.Sessions
{
    public class AddSessionPayload : Payload
    {
        public AddSessionPayload(Session session)
        {
            Session = session;
        }

        public AddSessionPayload(UserError error) :
            base(new[] { error })
        {

        }

        public Session? Session { get; }
    }
}
