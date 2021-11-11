using Graphql.Server.Data;
using HotChocolate.Types.Relay;

namespace Graphql.Server.Tracks
{
    public record RenameTrackInput([ID(nameof(Track))] int Id, string Name);
}
