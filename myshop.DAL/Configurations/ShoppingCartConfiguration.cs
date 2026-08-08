using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.DAL.Configurations
{
    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.HasOne(sc => sc.Product)
                .WithMany(p => p.ShoppingCarts)
                .HasForeignKey(sc => sc.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(sc => sc.ApplicationUser)
               .WithMany(p => p.ShoppingCarts)
               .HasForeignKey(sc => sc.ApplicationUserId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
