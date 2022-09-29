using Cefalo.farhadcodes_a_CP_blog.Service.DTO.Story;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.farhadcodes_a_CP_blog.Service.Formatters
{
    public class CSVOutputFormatter: TextOutputFormatter
    {
        public CSVOutputFormatter()
        {
            SupportedMediaTypes.Add("text/csv");
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type type)
        {
            if (typeof(ShowStoryDTO).IsAssignableFrom(type) || typeof(IEnumerable<ShowStoryDTO>).IsAssignableFrom(type))
                return base.CanWriteType(type);
            return false;
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<ShowStoryDTO>)
            {
                IEnumerable<ShowStoryDTO> posts = (IEnumerable<ShowStoryDTO>)context.Object;
                foreach (ShowStoryDTO post in posts)
                {
                    ConvertToCsv(buffer, post);
                }
            }
            else
            {
                ConvertToCsv(buffer, (ShowStoryDTO)context.Object);
            }
            await response.WriteAsync(buffer.ToString());
        }

        private static void ConvertToCsv(StringBuilder buffer, ShowStoryDTO post)
        {
            buffer.AppendLine($"{post.Id},{post.AuthorID},{post.Title},{post.Description},{post.Topic},{post.Difficulty},{post.CreationTime},{post.LastModifiedTime}");
        }
    }
}
