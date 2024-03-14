using BookXChangeApi.Controllers.Interfaces;
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
        [SwaggerOperation("Get Categories", "Get a list of book categories.")]
        [SuccessResponse(type: typeof(List<GetCategoryDTO>))]
        public async Task<ActionResult<List<GetCategoryDTO>>> GetCategory()
        {
            var rates = await BL.GetCategoriesAsync();

            return Ok(rates);
        }

        [HttpPost]
        [SwaggerOperation("Add a book category", "Add a new category.")]
        [SuccessResponse("Category Added.")]
        public async Task<ActionResult> AddCategory(AddCategoryDTO form)
        {
            await BL.AddCategoryAsync(form);

            return Ok();
        }

    }
}