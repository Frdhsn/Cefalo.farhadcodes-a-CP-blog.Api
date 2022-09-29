﻿using Cefalo.farhadcodes_a_CP_blog.Database.Models;
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
    public class HtmlOutputFormatter: TextOutputFormatter
    {
        public HtmlOutputFormatter()
        {
            SupportedMediaTypes.Add("text/html");
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
                    ConvertToHtml(buffer, post);
                }
            }
            else
            {
                ConvertToHtml(buffer, (ShowStoryDTO)context.Object);
            }
            await response.WriteAsync(buffer.ToString());
        }

        private static void ConvertToHtml(StringBuilder buffer, ShowStoryDTO story)
        {
                buffer.AppendLine($"<p><h4>Id: {story.Id}</h4></p>");
                buffer.AppendLine($"<p><h4>Title: {story.Title}</h4></p>");
                buffer.AppendLine($"<p><h2>Authorname: {story.AuthorID}</h2></p>");
                buffer.AppendLine($"<p>Description: {story.Description}</p>");
                buffer.AppendLine($"<p><small>Created At: {story.CreationTime}</small></p>");
                buffer.AppendLine($"<p><small>Modified At: {story.LastModifiedTime}</small></p>");
                buffer.AppendLine();
        }
    }
}
