using Graphql.Server.Common;
using Graphql.Server.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graphql.Server.Sessions
{
    public class SessionPayloadBase : Payload
    {
        protected SessionPayloadBase(Session session)
        {
            Session = session;
        }

        protected SessionPayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {

        }

        public Session? Session { get; }
    }
}
