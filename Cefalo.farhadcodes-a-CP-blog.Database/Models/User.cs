using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.farhadcodes_a_CP_blog.Database.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public DateTime CreationTime { get; set; }
        public DateTime LastModifiedTime { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }


        public virtual ICollection<Story?> Stories { get; set; }
    }
}
