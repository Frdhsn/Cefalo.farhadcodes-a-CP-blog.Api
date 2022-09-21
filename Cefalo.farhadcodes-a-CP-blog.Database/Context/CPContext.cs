using Cefalo.farhadcodes_a_CP_blog.Database.Configurations;
using Cefalo.farhadcodes_a_CP_blog.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.farhadcodes_a_CP_blog.Database.Context
{
    public class CPContext : DbContext
    {
        public CPContext() { }
        public CPContext(DbContextOptions<CPContext> options): base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            new UserConfiguration().Configure(builder.Entity<User>());
        }
    }
}
