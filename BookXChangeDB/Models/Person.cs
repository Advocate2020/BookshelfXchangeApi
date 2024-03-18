using Batsamayi.Shared.BL.Extensions;
using System.ComponentModel.DataAnnotations;

namespace BookXChangeDB.Models
{
    public class Person : IDbEntity
    {
        public Person(int id, string? firstName, string? lastName, string? phoneNumber, string? email, DateTime dateCreated, DateTime dateModified)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            DateCreated = dateCreated;
            DateModified = dateModified;
        }

        public Person()
        {
            DateCreated = DateTime.UtcNow;
            DateModified = null;
        }

        [Key]
        public int Id { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

    }
}
