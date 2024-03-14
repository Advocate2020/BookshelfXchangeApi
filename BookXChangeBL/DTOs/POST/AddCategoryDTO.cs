using BookXChangeDB.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace BookXChangeBL.DTOs.POST
{
    public class AddCategoryDTO
    {

        [Required]
        [SwaggerSchema("The name of the category.")]
        public string Name { get; set; }


        public Category Map()
        {
            return new Category
            {
                Name = Name
            };
        }
    }
}
