using Swashbuckle.AspNetCore.Annotations;

namespace BookXChangeBL.DTOs.GET
{
    public class GetBookDTO
    {
        [SwaggerSchema("The id of the cbook.")]
        public required int Id { get; set; }

        [SwaggerSchema("The title of the book.")]
        public required string Title { get; set; }
        [SwaggerSchema("The author of the book.")]
        public required string Author { get; set; }
        [SwaggerSchema("The category id of the book.")]
        public required int CategoryId { get; set; }
        [SwaggerSchema("The category name of the book.")]
        public required string CategoryName { get; set; }

    }
}
