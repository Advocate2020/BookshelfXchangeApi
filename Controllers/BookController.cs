using Batsamayi.Shared.BL.ExceptionHandling;
using BookXChangeApi.Controllers.Interfaces;
using BookXChangeApi.DTOs;
using BookXChangeApi.Util.Swagger.SwaggerResponseAttributes;
using BookXChangeBL.DTOs.GET;
using BookXChangeBL.DTOs.UPDATE;
using BookXChangeBL.Logic.BookNS;
using BookXChangeDB.Databases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace BookXChangeApi.Controllers
{
    public class BookController : BookXChangeController
    {
        public BookBL BL;

        public BookController(DatabaseContext dbContext, IDbContextFactory<DatabaseContext> contextFactory) : base(dbContext, contextFactory)
        {
            BL = new BookBL(contextFactory, dbContext);
        }

        [HttpGet]
        [SwaggerOperation("Get Books", "Get a list of books.")]
        [SuccessResponse(type: typeof(List<GetBookDTO>))]
        public async Task<ActionResult<List<GetBookDTO>>> GetBooks()
        {
            var books = await BL.GetBooksAsync();

            return Ok(books);
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Get Book", "Get a book by id.")]
        [SuccessResponse(type: typeof(GetBookDTO))]
        public async Task<ActionResult<GetBookDTO>> GetBookById(int id)
        {
            var books = await BL.GetBookByIdAsync(id);

            return Ok(books);
        }

        [HttpPost]
        [SwaggerOperation("Add a book", "Add a new book.")]
        [SuccessResponse("Book Added.")]
        public async Task<ActionResult> AddBook(AddBookDTO form)
        {
            await BL.AddBookAsync(form);

            return Ok();
        }

        [HttpPut("{id}")]
        [SwaggerOperation("Update a book", "Update an existing book.")]
        [SuccessResponse("Book Updated.")]
        public async Task<ActionResult> UpdateBook(int id, UpdateBookDTO form)
        {
            try
            {
                await BL.UpdateBookAsync(id, form);
                return Ok();
            }
            catch (ClientError ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation("Delete Book", "Delete a book by id.")]
        [SuccessResponse("Book Deleted.")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            try
            {
                // Call the delete method from your business logic layer
                await BL.DeleteBookAsync(id);

                // Return a success response
                return Ok();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"Error occurred while deleting the book: {ex.Message}");

                // Optionally, return an error response
                return StatusCode(500, "An error occurred while deleting the book.");
            }
        }

    }
}