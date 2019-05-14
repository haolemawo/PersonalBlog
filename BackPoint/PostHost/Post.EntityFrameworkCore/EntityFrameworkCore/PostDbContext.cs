using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Post.Core.ArticleManager.ArticleEntity;
using Post.Core.ArticleManager.ArticleTypeEntity;
using Post.Core.MessageManager.LeaveMessageEntity;
using Post.EntityFrameworkCore.EntityConfiguration.ArticleConfig;
using Post.EntityFrameworkCore.EntityConfiguration.LeaveMessageConfig;

namespace Post.EntityFrameworkCore.EntityFrameworkCore
{
    public class PostDbContext : AbpDbContext
    {
        //添加表
        //文章表
        public DbSet<Article> Articles { get; set; }
        //留言表
        public DbSet<LeaveMessage> LeaveMessages { get; set; }
        
        //文章标签表
        public DbSet<ArticleType> ArticleTags { get; set; }

        public PostDbContext(DbContextOptions<PostDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //添加FluentAPI设置表
            modelBuilder.ApplyConfiguration(new ArticleConfiguration());
            modelBuilder.ApplyConfiguration(new ArticleTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LeaveMessageConfiguration());
        }
    }
}
