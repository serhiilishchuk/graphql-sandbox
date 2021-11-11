using Graphql.Server.Attendees;
using Graphql.Server.Data;
using Graphql.Server.DataLoader;
using Graphql.Server.Sessions;
using Graphql.Server.Tracks;
using Graphql.Server.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Graphql.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPooledDbContextFactory<AppDbContext>(opt => opt.UseSqlite("Data Source=conferences.db"));

            services
                .AddGraphQLServer()
                .AddQueryType(d => d.Name("Query"))
                    .AddTypeExtension<SessionQuery>()
                    .AddTypeExtension<SpeakersQuery>()
                    .AddTypeExtension<TrackQuery>()
                    .AddTypeExtension<AttendeeQueries>()
                .AddMutationType(d => d.Name("Mutation"))
                    .AddTypeExtension<SessionMutations>()
                    .AddTypeExtension<SpeakersMutation>()
                    .AddTypeExtension<TrackMutations>()
                    .AddTypeExtension<AttendeeMutations>()
                .AddSubscriptionType(d => d.Name("Subscription"))
                    .AddTypeExtension<AttendeeSubscriptions>()
                    .AddTypeExtension<SessionSubscriptions>()
                .AddType<AttendeeType>()
                .AddType<SessionType>()
                .AddType<SpeakerType>()
                .AddType<TrackType>()
                .AddType<SpeakerType>()
                .EnableRelaySupport()
                .AddFiltering()
                .AddSorting()
                .AddInMemorySubscriptions()
                .AddDataLoader<SpeakerByIdDataLoader>()
                .AddDataLoader<SessionByIdDataLoader>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWebSockets();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }
    }
}
