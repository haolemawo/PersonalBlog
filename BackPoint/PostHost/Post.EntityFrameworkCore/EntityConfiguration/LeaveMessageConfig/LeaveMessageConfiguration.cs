using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Post.Core.MessageManager.LeaveMessageEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Post.EntityFrameworkCore.EntityConfiguration.LeaveMessageConfig
{
    public class LeaveMessageConfiguration : IEntityTypeConfiguration<LeaveMessage>
    {
        public void Configure(EntityTypeBuilder<LeaveMessage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.UserAvator)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(x => x.CreationTime)
                .IsRequired();
            builder.Property(x => x.MessageContent)
                .IsRequired()
                .HasMaxLength(600);
        }
    }
}
