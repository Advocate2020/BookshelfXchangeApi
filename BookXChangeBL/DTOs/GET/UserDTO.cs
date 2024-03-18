using System.ComponentModel.DataAnnotations;

namespace BookXChangeBL.DTOs.GET
{
    public class RegisterUserDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(8, ErrorMessage = "Your password should be 8 characters long.")]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        //public User Map()
        //{
        //    return new User
        //    {
        //        Email = Email,
        //        FirstName = FirstName,
        //        LastName = LastName,

        //        UserName = Email
        //    };
        //}
    }
}


