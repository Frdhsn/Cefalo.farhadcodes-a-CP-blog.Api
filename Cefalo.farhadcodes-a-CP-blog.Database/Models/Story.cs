using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.farhadcodes_a_CP_blog.Database.Models
{
    public class Story
    {
        public int Id { get; set; }
        public int AuthorID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Topic { get; set; }
        public string Difficulty { get; set; }

        public DateTime CreationTime { get; set; }
        public DateTime LastModifiedTime { get; set; }

        public virtual User User { get; set; }

    }
}
