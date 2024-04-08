using Batsamayi.Shared.BL.BusinessLayerNS;
using Batsamayi.Shared.BL.ExceptionHandling;
using Batsamayi.Shared.BL.Extensions;
using BookXChangeApi.DTOs;
using BookXChangeBL.DTOs.GET;
using BookXChangeBL.DTOs.UPDATE;
using BookXChangeDB.Databases;
using Firebase.Storage;
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

        public async Task AddBookAsync(AddBookDTO form, string idToken)
        {
            var firebaseStorage = new FirebaseStorage(
        "bookshelfxchange.appspot.com",
        new FirebaseStorageOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(idToken),

        });

            await ContextFactory
            .CreateDbContextAsync()
            .WithDefaultTimeout()
            .ExecuteWithTransaction(async (tContext) =>
            {
                // Check if a book with the same name and author already exists
                var existingBook = await _Queries.GetBooks()
                    .FirstOrDefaultAsync(b => b.Title == form.Title && b.Author == form.Author);

                if (existingBook != null)
                {
                    throw new ClientError("Book already exists.");
                }
                // Map the form data to a Book entity asynchronously
                var book = form.Map();

                tContext.Add(book);

                await tContext.SaveChangesAsync();



                // Upload each provided image to Firebase Storage with file name and book ID
                foreach (var formFile in form.BookImages)
                {
                    try
                    {
                        // Generate a unique file name based on the book ID and the original file name
                        string uniqueFileName = $"{book.Id}_{formFile.FileName}";

                        // Upload the image file to Firebase Storage
                        var fileStream = formFile.OpenReadStream();
                        var response = await firebaseStorage.Child("book_images").Child(uniqueFileName).PutAsync(fileStream);


                    }

                    catch (FirebaseStorageException ex)
                    {
                        throw new ClientError($"Failed to upload files.{ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        throw new ClientError("Failed to upload files.");
                    }
                }
            });
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
