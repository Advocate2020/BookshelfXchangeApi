using BookXChangeApi.Util.Swagger.SwaggerResponseAttributes.Constants;
using Swashbuckle.AspNetCore.Annotations;

namespace BookXChangeApi.Util.Swagger.SwaggerResponseAttributes
{
    public class ForbiddenResponseAttribute : SwaggerResponseAttribute
    {
        public ForbiddenResponseAttribute()
            : base(403, ApiMessage.Forbidden)
        {
        }
    }
}