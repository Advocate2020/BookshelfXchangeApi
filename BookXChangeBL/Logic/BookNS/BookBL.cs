using Batsamayi.Shared.BL.BusinessLayerNS;
using Batsamayi.Shared.BL.ExceptionHandling;
using Batsamayi.Shared.BL.Extensions;
using BookXChangeApi.DTOs;
using BookXChangeBL.DTOs.GET;
using BookXChangeBL.DTOs.UPDATE;
using BookXChangeDB.Databases;
using Microsoft.EntityFrameworkCore;

namespace BookXChangeBL.Logic.BookNS
{
    public class BookBL : BL<DatabaseContext>
    {
        private readonly BookQueries _Queries;

        public BookBL(IDbContextFactory<DatabaseContext> contextFactory, DatabaseContext context) : base(contextFactory, context)
        {
            _Queries = new BookQueries(context);
        }

        public async Task<List<GetBookDTO>> GetBooksAsync()
        {
            var books = await _Queries
                .GetBooks()
                .ToListAsync();

            return books;
        }

        public async Task<GetBookDTO> GetBookByIdAsync(int id)
        {
            var book = await _Queries.GetBookByIdAsync(id).FirstOrDefaultAsync();

            return book ?? throw new ClientError("Book waas not found.");
        }

        public async Task AddBookAsync(AddBookDTO form)
        {
            await using var tContext = await ContextFactory.CreateDbContextAsync();
            await using var transaction = await tContext.BeginDefaultTransactionAsync();

            try
            {
                // Check if a book with the same name and author already exists
                var existingBook = await _Queries.GetBooks()
                    .FirstOrDefaultAsync(b => b.Title == form.Title && b.Author == form.Author);

                if (existingBook != null)
                {
                    throw new ClientError("Book already exists.");
                }

                var book = form.Map();

                await tContext.AddAndSaveAsync(book);
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task UpdateBookAsync(int id, UpdateBookDTO form)
        {
            await using var tContext = await ContextFactory.CreateDbContextAsync();
            await using var transaction = await tContext.BeginDefaultTransactionAsync();

            try
            {

                var existingBook = await new BookQueries(tContext)
                                             .GetBookById(id)
                                             .FirstOrDefaultAsync()
                                             ?? throw new ClientError("Book not found.");
                form.Map(existingBook);


                await tContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteBookAsync(int id)
        {
            await using var tContext = await ContextFactory.CreateDbContextAsync();
            await using var transaction = await tContext.BeginDefaultTransactionAsync();

            try
            {
                var bookToDelete = await tContext.Books.FindAsync(id);
                if (bookToDelete == null)
                {
                    throw new ClientError("Book not found.");
                }

                tContext.Books.Remove(bookToDelete);
                await tContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

    }
}
