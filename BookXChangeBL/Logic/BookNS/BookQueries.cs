using Batsamayi.Shared.BL.Queries;
using BookXChangeBL.DTOs.GET;
using BookXChangeDB.Databases;
using BookXChangeDB.Models;

namespace BookXChangeBL.Logic.BookNS
{
    internal class BookQueries : Queries<DatabaseContext>
    {
        public BookQueries(DatabaseContext context) : base(context)
        {
        }

        internal IQueryable<GetBookDTO> GetBooks()
        {
            return _context.Books
                .Select(b => new GetBookDTO()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Description = b.Description,
                    PublishDate = b.PublishDate,
                    CategoryId = b.CategoryId,
                    CategoryName = b.Category.Name,

                });
        }


        internal IQueryable<GetBookDTO> GetBookByIdAsync(int id)
        {
            return _context.Books
                .Where(b => b.Id == id)
                .Select(b => new GetBookDTO()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Description = b.Description,
                    PublishDate = b.PublishDate,
                    CategoryId = b.CategoryId,
                    CategoryName = b.Category.Name,
                });
        }

        internal IQueryable<Book> GetBookById(int id)
        {
            return _context.Books
               .Where(b => b.Id == id);
        }

    }
}