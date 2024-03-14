using Batsamayi.Shared.BL.Extensions;
using System.ComponentModel.DataAnnotations;

namespace BookXChangeDB.Models
{
    public class Category : IDbEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation property
        public ICollection<Book>? Books { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public Category(int id, string name, DateTime dateCreated, DateTime? dateModified)
        {
            Id = id;
            Name = name;
            DateCreated = dateCreated;
            DateModified = dateModified;
        }

        public Category()
        {
            DateCreated = DateTime.UtcNow;
        }
    }

}
