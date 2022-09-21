using Cefalo.farhadcodes_a_CP_blog.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.farhadcodes_a_CP_blog.Database.Configurations
{
    public class UserConfiguration: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.HasKey(user => user.Id);
            builder.Property(user => user.Id).IsRequired();
            builder.HasIndex(user => user.Id).IsUnique();

            builder.Property(user => user.Email).IsRequired().HasMaxLength(256);
            builder.HasIndex(user => user.Email).IsUnique();
            
            builder.Property(user => user.CreationTime).IsRequired();
            
            builder.Property(user => user.LastModifiedTime).IsRequired();
        }
    }
}
