using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using myshop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.DAL.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasOne(a => a.ApplicationUser)
                .WithMany(r => r.Reviews)
                .HasForeignKey(a => a.userId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.Product)
                .WithMany(r => r.Reviews)
                .HasForeignKey(a => a.productId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
