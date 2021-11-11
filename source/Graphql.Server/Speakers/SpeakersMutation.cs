using Graphql.Server.Common.Speakers;
using Graphql.Server.Data;
using Graphql.Server.Extensions;
using HotChocolate;
using HotChocolate.Types;
using System.Threading.Tasks;

namespace Graphql.Server
{
    [ExtendObjectType("Mutation")]
    public class SpeakersMutation
    {
        [UseApplicationDbContext]
        public async Task<AddSpeakerPayload> AddSpeakerAsync(AddSpeakerInput input,  [ScopedService] AppDbContext context)
        {
            var speaker = new Speaker()
            {
                Bio = input.Bio,
                Name = input.Name,
                WebSite = input.WebSite
            };

            context.Speakers.Add(speaker);
            await context.SaveChangesAsync();

            return new AddSpeakerPayload(speaker);
        }
    }
}
