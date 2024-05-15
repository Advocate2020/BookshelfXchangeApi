using BookXChangeApi.Controllers.Interfaces;
using BookXChangeApi.Util.Swagger;
using BookXChangeApi.Util.Swagger.SwaggerResponseAttributes;
using BookXChangeBL.DTOs.GET;
using BookXChangeBL.DTOs.POST;
using BookXChangeBL.Logic.CategoryNS;
using BookXChangeDB.Databases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace BookXChangeApi.Controllers
{
    public class CategoryController : BookXChangeController
    {
        public CategoryBL BL;

        public CategoryController(DatabaseContext dbContext, IDbContextFactory<DatabaseContext> contextFactory) : base(dbContext, contextFactory)
        {
            BL = new CategoryBL(contextFactory, dbContext);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get Categories", Description = "Get a list of book categories.", Tags = new[] { BookshelfXCTags.Category })]
        [SuccessResponse(type: typeof(List<GetCategoryDTO>))]
        public async Task<ActionResult<List<GetCategoryDTO>>> GetCategory()
        {
            var rates = await BL.GetCategoriesAsync();

            return Ok(rates);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add a book category", Description = "Add a new category.", Tags = new[] { BookshelfXCTags.Category })]
        [SuccessResponse("Category Added.")]
        public async Task<ActionResult> AddCategory(AddCategoryDTO form)
        {
            await BL.AddCategoryAsync(form);

            return Ok();
        }

    }
}