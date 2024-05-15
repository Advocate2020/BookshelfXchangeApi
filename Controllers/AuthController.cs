using BookXChangeApi.Controllers.Interfaces;
using BookXChangeApi.Util.Swagger;
using BookXChangeApi.Util.Swagger.SwaggerResponseAttributes;
using BookXChangeBL.DTOs.POST;
using BookXChangeBL.Logic.FirebaseNS;
using BookXChangeBL.Logic.UserNS;
using BookXChangeDB.Databases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace BookXChangeApi.Controllers
{
    public class AuthController : BookXChangeController
    {
        private readonly UserBL _userBL;


        public AuthController(DatabaseContext dbContext, IDbContextFactory<DatabaseContext> contextFactory) : base(dbContext, contextFactory)
        {
            _userBL = new UserBL(contextFactory, dbContext);

        }

        [HttpPost]
        [Authorize]
        [SwaggerOperation(Summary = "Add a user", Description = "Add a new user.", Tags = new[] { BookshelfXCTags.Auth })]
        [SuccessResponse("User registered successfully.")]
        public async Task<IActionResult> Profile(AddUserDTO profile)
        {
            var firebaseId = await FirebaseService.GetFirebaseIdAsync(Request);
            var userEmail = await FirebaseService.GetUserEmail(firebaseId);

            await _userBL.AddUserAsync(profile, firebaseId, Request, userEmail);

            return Ok("Refresh session in order to gain access to new account.");
        }
    }
}
