﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.farhadcodes_a_CP_blog.Service.DTO.Story
{
    public class UpdateStory
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Topic { get; set; }
        public string? Difficulty { get; set; }
    }
}
