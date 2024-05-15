using BookXChangeDB.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace BookXChangeBL.DTOs.UPDATE
{
    public class UpdateBookDTO
    {
        [SwaggerSchema("The title of the book.")]
        public required string Title { get; set; }

        [SwaggerSchema("The author of the book.")]
        public required string Author { get; set; }

        [SwaggerSchema("The description of the book.")]
        public string Description { get; set; }

        [SwaggerSchema("The publish date of the book.")]
        public DateTime PublishDate { get; set; }

        [SwaggerSchema("The id of the category.")]
        public required int CategoryId { get; set; }

        public void Map(Book book)
        {

            book.Title = Title;
            book.Author = Author;
            book.Description = Description;
            book.PublishDate = PublishDate;
            book.CategoryId = CategoryId;
            book.DateModified = DateTime.UtcNow;
        }
    }
}
