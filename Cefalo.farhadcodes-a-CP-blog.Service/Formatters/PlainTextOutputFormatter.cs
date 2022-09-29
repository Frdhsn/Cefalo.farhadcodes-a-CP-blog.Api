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
    public class PlainTextOutputFormatter: TextOutputFormatter
    {
        public PlainTextOutputFormatter()
        {
            SupportedMediaTypes.Add("text/plain");
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
                    ConvertToPlain(buffer, post);
                }
            }
            else
            {
                ConvertToPlain(buffer, (ShowStoryDTO)context.Object);
            }
            await response.WriteAsync(buffer.ToString());
        }

        private static void ConvertToPlain(StringBuilder buffer, ShowStoryDTO story)
        {
            buffer.AppendLine($"Story Id: {story.Id}");
            buffer.AppendLine($"Title: {story.Title}");
            buffer.AppendLine($"AuthorID: {story.AuthorID}");
            buffer.AppendLine($"Description: {story.Description}");
            buffer.AppendLine($"Created At: {story.CreationTime}");
            buffer.AppendLine($"Modified At: {story.LastModifiedTime}");
            buffer.AppendLine();
        }
    }
}
