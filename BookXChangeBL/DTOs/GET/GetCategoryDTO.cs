
using Swashbuckle.AspNetCore.Annotations;

namespace BookXChangeBL.DTOs.GET
{
	public class GetCategoryDTO
	{
		[SwaggerSchema("The id of the category.")]
		public required int Id { get; set; }
		[SwaggerSchema("The name of the category.")]
		public required string Name { get; set; }
		[SwaggerSchema("The number of books under this category.")]
		public required int BookCount { get; set; }
	}


}
