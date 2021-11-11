using Graphql.Server.Common;
using Graphql.Server.Data;
using System.Collections.Generic;

namespace Graphql.Server.Attendees
{
    public class AttendeePayloadBase : Payload
    {
        protected AttendeePayloadBase(Attendee attendee)
        {
            Attendee = attendee;
        }

        protected AttendeePayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {

        }

        public Attendee? Attendee { get; }
    }
}
