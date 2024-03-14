using BookXChangeApi.Util.Swagger.SwaggerResponseAttributes.Constants;
using Swashbuckle.AspNetCore.Annotations;

namespace BookXChangeApi.Util.Swagger.SwaggerResponseAttributes
{
    public class UnhandledExceptionResponseAttribute : SwaggerResponseAttribute
    {
        public UnhandledExceptionResponseAttribute()
            : base(500, ApiMessage.UnhandledException)
        {
        }
    }
}