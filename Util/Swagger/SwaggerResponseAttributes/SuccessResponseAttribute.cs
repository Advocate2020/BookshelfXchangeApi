using Swashbuckle.AspNetCore.Annotations;

namespace BookXChangeApi.Util.Swagger.SwaggerResponseAttributes
{
    public class SuccessResponseAttribute : SwaggerResponseAttribute
    {
        public SuccessResponseAttribute(string? message = null, Type? type = null)
            : base(200, message, type)
        {
        }
    }
}