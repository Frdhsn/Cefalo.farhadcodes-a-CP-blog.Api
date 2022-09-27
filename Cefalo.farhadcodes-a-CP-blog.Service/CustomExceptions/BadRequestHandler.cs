using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.farhadcodes_a_CP_blog.Service.CustomExceptions
{
    public class BadRequestHandler : Exception
    {
        public BadRequestHandler() { }
        public BadRequestHandler(string message) : base(message) { }
    }
}
