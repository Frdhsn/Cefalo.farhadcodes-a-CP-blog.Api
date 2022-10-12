using Cefalo.farhadcodes_a_CP_blog.Service.CustomExceptions;
using FluentValidation;
//using Microsoft.IdentityModel.SecurityTokenService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.farhadcodes_a_CP_blog.Service.DTOValidators
{
    public class BaseDTOValidator<T> : AbstractValidator<T>
    {
        public virtual void ValidateDTO(T DTO)
        {
            var result = this.Validate(DTO);
            string err = "";
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    err += $"{error.PropertyName}: {error.ErrorMessage}\n";
                }
                throw new BadRequestHandler(err);
            }
        }
    }
}