using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Post.Core;
using Post.Core.Configuration;
using Post.Core.Web;

namespace Post.EntityFrameworkCore.EntityFrameworkCore
{
    public class PostDbContextFactory : IDesignTimeDbContextFactory<PostDbContext>
    {
        public PostDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<PostDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            DbContextOptionsConfigurer.Configure(
                builder,
                configuration.GetConnectionString(PostConsts.ConnectionStringName)
            );

            return new PostDbContext(builder.Options);
        }
    }
}
