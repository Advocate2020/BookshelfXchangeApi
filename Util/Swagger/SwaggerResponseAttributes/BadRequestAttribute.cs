using BookXChangeApi.Util.Swagger.SwaggerResponseAttributes.Constants;
using Swashbuckle.AspNetCore.Annotations;

namespace BookXChangeApi.Util.Swagger.SwaggerResponseAttributes
{
    public class BadRequestResponseAttribute : SwaggerResponseAttribute
    {
        public BadRequestResponseAttribute()
            : base(400, ApiMessage.BadRequest)
        {
        }
    }
}