using System.Text.Json;

namespace Cefalo.farhadcodes_a_CP_blog.Api.ErrorHandler
{
    public class ErrorDetails
    {
        public int StatusCode;
        public string Message;
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
