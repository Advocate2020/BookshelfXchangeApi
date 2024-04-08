using BookXChangeDB.Models;
using Microsoft.AspNetCore.Http;
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
        [SwaggerSchema("The id of the category. 1 = Fiction, 2 = Non-Fiction, 3 = Science Fiction, 4 = Modern Literature, 5 = Coming of Age, 6 = Fantasy, 7 = Epic Fiction, 8 = Mystery, 9 = Thriller ")]

        public int CategoryId { get; set; }

        [Required]
        [SwaggerSchema("The images associated with the book.")]
        public ICollection<IFormFile> BookImages { get; set; }


        public Book Map()
        {
            return new Book
            {
                Title = Title,
                Author = Author,
                CategoryId = CategoryId,
            };
        }

    }
}
