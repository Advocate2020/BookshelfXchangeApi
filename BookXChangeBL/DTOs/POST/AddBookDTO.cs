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

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(150, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string Author { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [SwaggerSchema("The id of the category. 1 = Fiction, 2 = Non-Fiction, 3 = Science Fiction, 4 = Modern Literature, 5 = Coming of Age, 6 = Fantasy, 7 = Epic Fiction, 8 = Mystery, 9 = Thriller ")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        public string Description { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }

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
                Description = Description,
                PublishDate = PublishDate,
            };
        }

    }
}
