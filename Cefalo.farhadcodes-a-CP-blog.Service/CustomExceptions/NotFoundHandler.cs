﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.farhadcodes_a_CP_blog.Service.CustomExceptions
{
    public class NotFoundHandler : Exception
    {
        public NotFoundHandler() { }
        public NotFoundHandler(string message) : base(message) { }
    }
}
