using Graphql.Server.Common;
using Graphql.Server.Data;
using Graphql.Server.Extensions;
using HotChocolate;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Graphql.Server.Sessions
{
    [ExtendObjectType("Mutation")]
    public class SessionMutations
    {
        [UseApplicationDbContext]
        public async Task<AddSessionPayload> AddSessionAsync(
            AddSessionInput input, [ScopedService] AppDbContext context, CancellationToken token)
        {
            if (string.IsNullOrEmpty(input.Title))
            {
                return new AddSessionPayload(new UserError("The title cannot be null", "TITLE_EMPTY"));
            }

            if (input.SpeakerIds.Count == 0)
            {
                return new AddSessionPayload(new UserError("No speakers assigned", "NO_SPEAKER"));
            }

            var session = new Session
            {
                Title = input.Title,
                Abstract = input.Abstract
            };

            foreach (var speakerId in input.SpeakerIds)
            {
                session.SessionSpeakers.Add(new SessionSpeaker
                {
                    SpeakerId = speakerId
                });
            }

            context.Sessions.Add(session);
            await context.SaveChangesAsync(token);

            return new AddSessionPayload(session);
        }

        [UseApplicationDbContext]
        public async Task<ScheduleSessionPayload> ScheduleSessionAsync(ScheduleSessionInput input, 
            [ScopedService] AppDbContext context, [Service] ITopicEventSender eventSender)
        {
            if (input.EndTime < input.StartTime)
            {
                return new ScheduleSessionPayload(
                    new UserError("endTime has to be larger than startTime.", "END_TIME_INVALID"));
            }

            Session session = await context.Sessions.FindAsync(input.SessionId);
            int? initialTrackId = session.TrackId;

            if (session is null)
            {
                return new ScheduleSessionPayload(
                    new UserError("Session not found.", "SESSION_NOT_FOUND"));
            }

            session.TrackId = input.TrackId;
            session.StartTime = input.StartTime;
            session.EndTime = input.EndTime;

            await context.SaveChangesAsync();

            await eventSender.SendAsync(nameof(SessionSubscriptions.OnSessionScheduledAsync), session.Id);

            return new ScheduleSessionPayload(session);
        }
    }
}
