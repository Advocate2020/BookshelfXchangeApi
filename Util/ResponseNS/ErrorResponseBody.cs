using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace BookXChangeApi.Util.ResponseNS
{
    public class ErrorResponseBody<T> where T : class?
    {
        [JsonPropertyName("status")]
        [SwaggerSchema("The response status. Can be error  or exception.")]
        public string? Status { get; set; }

        [JsonPropertyName("message")]
        [SwaggerSchema("This message is used to show a user error message during a status code 400 (bad request).")]
        public string? Message { get; set; }

        [JsonPropertyName("debug-message")]
        [SwaggerSchema("An exception message.")]
        public string? DebugMessage { get; set; }

        public ErrorResponseBody(string message, ErrorResponseType type)
        {
            Message = message;
            SetStatus(type);
        }

        private void SetStatus(ErrorResponseType type)
        {
            Status = type switch
            {
                ErrorResponseType.Error => "error",
                ErrorResponseType.Exception => "exception",
                ErrorResponseType.NotImplemented => "not implemented",
                _ => Status,
            };
        }
    }
}
