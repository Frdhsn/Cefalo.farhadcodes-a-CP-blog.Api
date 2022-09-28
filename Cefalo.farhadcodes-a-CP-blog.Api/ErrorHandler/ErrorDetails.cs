using System.Text.Json;

namespace Cefalo.farhadcodes_a_CP_blog.Api.ErrorHandler
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
