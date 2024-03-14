using BookXChangeApi.Util.Swagger.SwaggerResponseAttributes.Constants;
using Swashbuckle.AspNetCore.Annotations;

namespace BookXChangeApi.Util.Swagger.SwaggerResponseAttributes
{
    public class UnauthorizedResponseAttribute : SwaggerResponseAttribute
    {
        public UnauthorizedResponseAttribute()
            : base(401, ApiMessage.Unauthorized)
        {
        }
    }
}