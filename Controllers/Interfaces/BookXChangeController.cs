using Batsamayi.Shared.API.Controllers;
using BookXChangeApi.Util.Swagger.SwaggerResponseAttributes;
using BookXChangeDB.Databases;
using Microsoft.EntityFrameworkCore;


namespace BookXChangeApi.Controllers.Interfaces
{

    [SuccessResponse]
    [BadRequestResponse]
    [ForbiddenResponse]
    [UnauthorizedResponse]
    [UnhandledExceptionResponse]
    public abstract class BookXChangeController : MainController<DatabaseContext>
    {
        protected BookXChangeController(DatabaseContext dbContext, IDbContextFactory<DatabaseContext> contextFactory) : base(dbContext, contextFactory)
        {
        }
    }
}