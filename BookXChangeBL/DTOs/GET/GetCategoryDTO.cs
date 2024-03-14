
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace BookXChangeBL.DTOs.GET
{
    public class GetCategoryDTO
    {
        [SwaggerSchema("The id of the category.")]
        public required int Id { get; set; }
        [SwaggerSchema("The name of the category.")]
        public required string Name { get; set; }       
    }
    
   
}
