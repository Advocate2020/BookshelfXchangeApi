using Batsamayi.Shared.BL.Queries;
using BookXChangeBL.DTOs.GET;
using BookXChangeDB.Databases;
using BookXChangeDB.Models;
using Microsoft.EntityFrameworkCore;

namespace BookXChangeBL.Logic.CategoryNS
{
	internal class CategoryQueries : Queries<DatabaseContext>
	{
		public CategoryQueries(DatabaseContext context) : base(context)
		{
		}

		internal IQueryable<GetCategoryDTO> GetCategories()
		{
			return _context.Categories
				.Include(c => c.Books)
				.Select(c => new GetCategoryDTO()
				{
					Id = c.Id,
					Name = c.Name,
					BookCount = c.Books.Count()
				});
		}


		internal IQueryable<Category> GetCategoryById(int id)
		{
			return _context.Categories
				.Where(c => c.Id == id);
		}
	}
}