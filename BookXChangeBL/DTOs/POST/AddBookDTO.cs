using BookXChangeDB.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace BookXChangeApi.DTOs
{
    public class AddBookDTO
    {

        [Required]
        [SwaggerSchema("The title of the book.")]
        public string? Title { get; set; }
        [Required]
        [SwaggerSchema("The author of the book.")]
        public string Author { get; set; }
        [Required]
        [SwaggerSchema("The id of the category.")]
        public int CategoryId { get; set; }

        public Book Map()
        {
            return new Book
            {
                Title = Title,
                Author = Author,
                CategoryId = CategoryId
            };
        }
    }
}
