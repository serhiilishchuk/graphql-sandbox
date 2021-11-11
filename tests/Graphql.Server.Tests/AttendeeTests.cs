using Graphql.Server.Attendees;
using Graphql.Server.Data;
using Graphql.Server.Types;
using HotChocolate;
using HotChocolate.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Snapshooter.Xunit;
using System.Threading.Tasks;
using Xunit;

namespace Graphql.Server.Tests
{
    public class AttendeeTests
    {
        [Fact]
        public async Task Attendee_Schema_Changed()
        {
            // arrange
            // act
            ISchema schema = await new ServiceCollection()
                .AddPooledDbContextFactory<AppDbContext>(
                    options => options.UseInMemoryDatabase("Data Source=conferences.db"))

                .AddGraphQL()
                .AddQueryType(d => d.Name("Query"))
                    .AddTypeExtension<AttendeeQueries>()
                .AddMutationType(d => d.Name("Mutation"))
                    .AddTypeExtension<AttendeeMutations>()
                .AddType<AttendeeType>()
                .AddType<SessionType>()
                .AddType<SpeakerType>()
                .AddType<TrackType>()
                .EnableRelaySupport()
                .BuildSchemaAsync();

            // assert
            schema.Print().MatchSnapshot();
        }

        [Fact]
        public async Task RegisterAttendee()
        {
            // arrange
            IRequestExecutor executor = await new ServiceCollection()
                .AddPooledDbContextFactory<AppDbContext>(
                    options => options.UseInMemoryDatabase("Data Source=conferences.db"))
                .AddGraphQL()
                .AddQueryType(d => d.Name("Query"))
                    .AddTypeExtension<AttendeeQueries>()
                .AddMutationType(d => d.Name("Mutation"))
                    .AddTypeExtension<AttendeeMutations>()
                .AddType<AttendeeType>()
                .AddType<SessionType>()
                .AddType<SpeakerType>()
                .AddType<TrackType>()
                .EnableRelaySupport()
                .BuildRequestExecutorAsync();

            // act
            IExecutionResult result = await executor.ExecuteAsync(@"
        mutation RegisterAttendee {
            registerAttendee(
                input: {
                        email: ""michael@chillicream.com""
                        firstName: ""michael""
                        lastName: ""staib""
                        userName: ""michael3""
                    })
            {
                attendee {
                    id
                }
            }
        }");

            // assert
            result.ToJson().MatchSnapshot();
        }
    }
}
