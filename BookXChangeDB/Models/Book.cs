using Batsamayi.Shared.BL.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookXChangeDB.Models
{
    public class Book : IDbEntity
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public DateTime PublishDate { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }



        public Book(int id, string title, string author, int categoryId, Category category, DateTime dateCreated, DateTime? dateModified)
        {
            Id = id;
            Title = title;
            Author = author;
            CategoryId = categoryId;
            Category = category;
            DateCreated = dateCreated;
            DateModified = dateModified;

        }


        public Book()
        {
            DateCreated = DateTime.UtcNow;
        }


    }
}
