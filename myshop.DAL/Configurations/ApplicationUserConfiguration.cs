using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.DAL.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
           
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            //builder.HasQueryFilter(u => u.IsDeleted == true);
        }
    }
}
