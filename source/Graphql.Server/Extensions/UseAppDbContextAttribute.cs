using System.Reflection;
using Graphql.Server.Data;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;

namespace Graphql.Server.Extensions
{
    public class UseApplicationDbContextAttribute : ObjectFieldDescriptorAttribute
    {
        public override void OnConfigure(IDescriptorContext context, IObjectFieldDescriptor descriptor, MemberInfo member)
        {
            descriptor.UseDbContext<AppDbContext>();
        }
    }
}