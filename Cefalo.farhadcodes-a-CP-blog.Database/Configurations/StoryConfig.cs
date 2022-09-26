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
    public class StoryConfig: IEntityTypeConfiguration<Story>
    {
        public void Configure(EntityTypeBuilder<Story> builder)
        {
            builder.HasKey(story => story.Id);

            builder.Property(story => story.Title).IsRequired().HasMaxLength(256);

            builder.Property(story => story.Description).IsRequired();

            builder.Property(story => story.CreationTime).IsRequired();

            builder.Property(story => story.LastModifiedTime).IsRequired();

            builder.HasOne<User>(story => story.User).WithMany(user => user.Stories).HasForeignKey(user => user.AuthorID);
        }

    }
}
