using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Post.Core.ArticleManager.ArticleEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Post.EntityFrameworkCore.EntityConfiguration.ArticleConfig
{
    /// <summary>
    /// 对Article表的列进行配置
    /// </summary>
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ArticleName)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(x => x.ArticleUrl)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(x => x.Author)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.Introduce)
                .IsRequired()
                .HasMaxLength(300);
            builder.Property(x => x.PageView)
                .IsRequired();
            builder.Property(x => x.CreationTime)
                .IsRequired();
            builder.Property(x => x.LastModificationTime)
                .IsRequired();

            builder.ToTable("Articles");
        }
    }
}
