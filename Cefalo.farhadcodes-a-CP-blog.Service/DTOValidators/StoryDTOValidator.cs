﻿using Cefalo.farhadcodes_a_CP_blog.Service.DTO.Story;
using Cefalo.farhadcodes_a_CP_blog.Service.DTOValidators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.farhadcodes_a_CP_blog.Service.DTOValidators
{
    public class StoryDTOValidator: BaseDTOValidator<StoryDTO>
    {
        public StoryDTOValidator()
        {

            RuleFor(x => x.Title).NotEmpty().WithMessage("Title can not be empty!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description can not be empty!");
            RuleFor(x => x.Topic).NotEmpty().WithMessage("Topic can not be empty!");
            RuleFor(x => x.Difficulty).NotEmpty().WithMessage("Difficulty can not be empty!");
            RuleFor(x => x.AuthorID).NotEmpty().WithMessage("AuthorID can not be empty!");
        }

    }
}
