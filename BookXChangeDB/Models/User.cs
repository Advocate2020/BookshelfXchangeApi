using Batsamayi.Shared.BL.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookXChangeDB.Models
{
    public class User : IDbEntity
    {
        public User(int id, string firebaseId, DateTime dateCreated, DateTime dateModified, int person)
        {
            Id = id;
            FirebaseId = firebaseId;
            DateCreated = dateCreated;
            DateModified = dateModified;
            PersonId = person;
        }

        public User(string firebaseId)
        {
            FirebaseId = firebaseId;
            DateCreated = DateTime.UtcNow;
        }

        [Key]
        public int Id { get; set; }

        public string FirebaseId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        [ForeignKey(nameof(Person))]
        public int? PersonId { get; set; }

        public Person? Person { get; set; }

        [ForeignKey(nameof(Role))]
        public int? RoleId { get; set; }

        public Role? Role { get; set; }
    }
}
