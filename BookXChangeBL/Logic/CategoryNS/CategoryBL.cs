using Batsamayi.Shared.BL.BusinessLayerNS;
using Batsamayi.Shared.BL.ExceptionHandling;
using Batsamayi.Shared.BL.Extensions;
using BookXChangeBL.DTOs.GET;
using BookXChangeBL.DTOs.POST;
using BookXChangeDB.Databases;
using Microsoft.EntityFrameworkCore;

namespace BookXChangeBL.Logic.CategoryNS
{
    public class CategoryBL : BL<DatabaseContext>
    {
        private readonly CategoryQueries _Queries;

        public CategoryBL(IDbContextFactory<DatabaseContext> contextFactory, DatabaseContext context) : base(contextFactory, context)
        {
            _Queries = new CategoryQueries(context);
        }

        public async Task<List<GetCategoryDTO>> GetCategoriesAsync()
        {
            var categories = await _Queries
                .GetCategories()
                .ToListAsync();

            return categories;
        }

        public async Task AddCategoryAsync(AddCategoryDTO form)
        {
            await using var tContext = await ContextFactory.CreateDbContextAsync();
            await using var transaction = await tContext.BeginDefaultTransactionAsync();

            try
            {
                // Check if a cvategory with the same name already exists
                var existingCategory = await _Queries.GetCategories()
                    .FirstOrDefaultAsync(b => b.Name == form.Name);

                if (existingCategory != null)
                {
                    throw new ClientError("Category already exists.");
                }

                var category = form.Map();

                await tContext.AddAndSaveAsync(category);
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
