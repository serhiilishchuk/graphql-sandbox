using Graphql.Server.Data;
using Graphql.Server.DataLoader;
using Graphql.Server.Extensions;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Graphql.Server
{
    [ExtendObjectType("Query")]
    public class SpeakersQuery
    {
        [UseApplicationDbContext]
        public Task<List<Speaker>> GetSpeakers([ScopedService] AppDbContext context) =>
            context.Speakers.ToListAsync();

        public Task<Speaker> GetSpeakerByIdAsync([ID(nameof(Speaker))] int id,
            SpeakerByIdDataLoader dataLoader, CancellationToken cancellationToken) =>
            dataLoader.LoadAsync(id, cancellationToken);

        public Task<IReadOnlyList<Speaker>> GetSpeakersByIdAsync([ID(nameof(Speaker))] int[] ids,
            SpeakerByIdDataLoader dataLoader, CancellationToken cancellationToken) =>
            dataLoader.LoadAsync(ids, cancellationToken);
    }
}
