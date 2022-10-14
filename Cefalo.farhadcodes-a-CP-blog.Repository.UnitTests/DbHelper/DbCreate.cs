using Cefalo.farhadcodes_a_CP_blog.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.farhadcodes_a_CP_blog.Repository.UnitTests.DbHelper
{
    public class DbCreate
    {
        private readonly CPContext _cpContext;
        public DbCreate()
        {
            var builder = new DbContextOptionsBuilder<CPContext>();
            builder.UseInMemoryDatabase("sweetDbOMine");

            var dbContextOptions = builder.Options;

            _cpContext = new CPContext(dbContextOptions);

            // delete existing db before creating a new one

            _cpContext.Database.EnsureDeleted();
            _cpContext.Database.EnsureCreated();
        }
    }
}
