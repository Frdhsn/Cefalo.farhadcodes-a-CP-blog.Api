using Cefalo.farhadcodes_a_CP_blog.Service.DTO.User;
using Cefalo.TechDaily.Service.DtoValidators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.farhadcodes_a_CP_blog.Service.DTOValidators
{
    public class UserDTOValidator: BaseDTOValidator<UserDTO>
    {
        public UserDTOValidator()
        {

            RuleFor(x => x.Name).NotEmpty().WithMessage("User name is required.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Invalid email format.");
        }
    }
}
