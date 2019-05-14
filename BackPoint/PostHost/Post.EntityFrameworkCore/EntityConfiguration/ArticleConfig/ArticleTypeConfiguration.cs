using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Post.Core.ArticleManager.ArticleEntity;
using Post.Core.ArticleManager.ArticleTypeEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Post.EntityFrameworkCore.EntityConfiguration.ArticleConfig
{
    public class ArticleTypeConfiguration : IEntityTypeConfiguration<ArticleType>
    {
        public void Configure(EntityTypeBuilder<ArticleType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.TypeName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.TypeValue)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.CreationTime)
                .IsRequired();

            //设置外键关系
            builder.HasMany(a => a.Articles)
                .WithOne(t => t.ArticleType)
                .HasPrincipalKey(t => t.Id)
                .HasForeignKey(a => a.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);//设置级联删除

            builder.ToTable("ArticleType");
        }
    }
}
