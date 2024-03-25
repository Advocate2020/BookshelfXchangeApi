using BookXChangeDB.Databases.BaseData;
using BookXChangeDB.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace BookXChangeBL.DTOs.POST
{
    public class AddUserDTO
    {
        [Required]
        [SwaggerSchema("The user's first name.")]
        public required string FirstName { get; set; }

        [Required]
        [SwaggerSchema("The user's last name.")]
        public required string LastName { get; set; }

        [Required]
        [RegularExpression(@"^(?:\+27|0)[1-9]\d{8}$", ErrorMessage = "Invalid South African phone number")]
        [SwaggerSchema("The user's phone number.")]
        public required string PhoneNumber { get; set; }

        public User Map(string firebaseId, string userEmail)
        {
            return new User(firebaseId)
            {
                RoleId = RoleData.USER.Id,

                Person = new Person
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = userEmail,
                    PhoneNumber = PhoneNumber
                }
            };
        }
    }
}
