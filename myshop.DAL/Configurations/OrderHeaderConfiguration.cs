using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.DAL.Configurations
{
    public class OrderHeaderConfiguration : IEntityTypeConfiguration<OrderHeader>
    {
        public void Configure(EntityTypeBuilder<OrderHeader> builder)
        {
            builder.HasOne(oh => oh.ApplicationUser)
                .WithMany(u => u.OrderHeaders)
                .HasForeignKey(oh => oh.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(t => t.TotalPrice).HasPrecision(18, 2);
        }
    }
}
