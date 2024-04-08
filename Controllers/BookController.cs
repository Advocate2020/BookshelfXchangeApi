using Batsamayi.Shared.BL.ExceptionHandling;
using BookXChangeApi.Controllers.Interfaces;
using BookXChangeApi.DTOs;
using BookXChangeApi.Util;
using BookXChangeApi.Util.Swagger;
using BookXChangeApi.Util.Swagger.SwaggerResponseAttributes;
using BookXChangeBL.DTOs.GET;
using BookXChangeBL.DTOs.UPDATE;
using BookXChangeBL.Logic.BookNS;
using BookXChangeBL.Logic.FirebaseNS;
using BookXChangeDB.Databases;
using Microsoft.AspNetCore.Authorization;
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
        [SwaggerOperation(
            Summary = "Get Books",
            Description = "Get a list of books.",
            Tags = new[] { BookshelfXCTags.Book })]

        [SuccessResponse(type: typeof(List<GetBookDTO>))]
        public async Task<ActionResult<List<GetBookDTO>>> GetBooks()
        {
            var books = await BL.GetBooksAsync();

            return Ok(books);
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(
            Summary = "Get Book",
            Description = "Get a book by its id.",
            Tags = new[] { BookshelfXCTags.Book })]
        [SuccessResponse(type: typeof(GetBookDTO))]
        public async Task<ActionResult<GetBookDTO>> GetBookById(int id)
        {
            var books = await BL.GetBookByIdAsync(id);

            return Ok(books);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(
            Summary = "Add a book",
            Description = "Add a new book.",
            Tags = new[] { BookshelfXCTags.Book })]
        [SuccessResponse("Book Added.")]
        public async Task<ActionResult> AddBook([FromForm] AddBookDTO form)
        {
            foreach (var image in form.BookImages)
            {
                FileUploadValidator.ValidateFile(image, FileExtensions.All);

            }

            var token = await FirebaseService.GetUsersTokenAsync(Request);

            await BL.AddBookAsync(form, token);

            return Ok("Book Added Successfully.");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(
            Summary = "Update a book",
            Description = "Update an existing book.",
            Tags = new[] { BookshelfXCTags.Book })]

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
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(
            Summary = "Delete Book",
            Description = "Delete a book by id.",
            Tags = new[] { BookshelfXCTags.Book })]

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