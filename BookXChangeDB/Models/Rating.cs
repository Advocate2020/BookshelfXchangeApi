using Batsamayi.Shared.BL.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookXChangeDB.Models
{

	public class Rating : IDbEntity
	{
		[Key]
		public int Id { get; set; }

		// Rating value (e.g., 1 to 5 stars)
		public int Value { get; set; }

		// Review or comment provided by the user
		public string Review { get; set; }

		// Foreign key to link to the Book
		[ForeignKey(nameof(Book))]
		public int BookId { get; set; }

		// Navigation property to access the associated Book
		public Book Book { get; set; }

		public int UserId { get; set; }

		public DateTime DateCreated { get; set; }

		public DateTime? DateModified { get; set; }

		public Rating()
		{
			DateCreated = DateTime.UtcNow;
		}

		public Rating(int id, int value, string review, int bookId, Book book, int userId, DateTime dateCreated, DateTime? dateModified)
		{
			Id = id;
			Value = value;
			Review = review;
			BookId = bookId;
			Book = book;
			UserId = userId;
			DateCreated = dateCreated;
			DateModified = dateModified;
		}
	}

}
