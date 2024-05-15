using Batsamayi.Shared.API.GeneralResponse;
using BookXChangeApi.Controllers.Interfaces;
using BookXChangeApi.Util.ApiKeyNS;
using BookXChangeApi.Util.Swagger;
using BookXChangeApi.Util.Swagger.SwaggerResponseAttributes;
using BookXChangeBL.Logic.FirebaseNS;
using BookXChangeDB.Databases;
using BookXChangeDB.Databases.BaseData;
using BookXChangeDB.Models;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace WowApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebSuperUserController : BookXChangeController
    {
        public WebSuperUserController(DatabaseContext dbContext, IDbContextFactory<DatabaseContext> contextFactory) : base(dbContext, contextFactory)
        {
        }

        /// <summary>
        /// Manually add a user on Firebase, and use their FirebaseId to give them access to the DOT admin role.
        /// </summary>
        [HttpPost("set-role-admin")]
        [ApiKeyRequired]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Make a user admin", Description = "A user becomes an admin.", Tags = new[] { BookshelfXCTags.WebSuperUser })]
        [SuccessResponse()]
        public async Task<IActionResult> SetRoleToDepartmentOfTransportAdminAsync(string firebaseUserId)
        {
            return await SetAdminRole(firebaseUserId, RoleData.ADMIN);
        }

        private async Task<IActionResult> SetAdminRole(string firebaseUserId, Role role)
        {
            var user = await FirebaseAuth.DefaultInstance.GetUserAsync(firebaseUserId);

            var userHasARole = user.CustomClaims.ContainsKey(ClaimTypes.Role);

            if (userHasARole)
            {
                return ErrorResponse.BadRequest("User has already been assigned to a role.");
            }

            var claims = new UserCustomClaims(0 /* We don't store a userId for admins */, role);

            // Set the user role. Please not that this will override any other roles that the user may have.
            await FirebaseService.SetAminUserClaims(firebaseUserId, claims);

            return Ok($"Firebase user role has been set to {role.Name}");
        }
    }
}