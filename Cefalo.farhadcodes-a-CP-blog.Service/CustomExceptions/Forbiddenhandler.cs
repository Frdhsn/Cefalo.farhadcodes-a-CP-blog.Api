using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.farhadcodes_a_CP_blog.Service.CustomExceptions
{
    public class ForbiddenHandler : Exception
    {
        public ForbiddenHandler() { }
        public ForbiddenHandler(string message) : base(message) { }
    }
}
