using BCXGoogle.SecretManagerNS;
using BookXChangeApi.Constants;
using BookXChangeApi.Controllers.Interfaces;
using BookXChangeApi.Util;
using BookXChangeApi.Util.ApiKeyNS;
using BookXChangeApi.Util.Swagger;
using BookXChangeApi.Util.Swagger.SwaggerResponseAttributes;
using BookXChangeDB.Databases;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace BookXChangeApi.Controllers
{
    public class DevController : BookXChangeController
    {
        private readonly GoogleSecretManagerService _sm;
        public DevController(DatabaseContext dbContext, IDbContextFactory<DatabaseContext> contextFactory) : base(dbContext, contextFactory)
        {
            _sm = new GoogleSecretManagerService();
        }



        [HttpPost("login")]
        [ApiKeyRequired]
        [AllowAnonymous]
        [SwaggerOperation(
            Summary = "Dev Login",
            Description = "Log in and get a Bearer Token. This endpoint should only be used for testing purposes.", Tags = new[] { BookshelfXCTags.Dev }
            )]
        [SuccessResponse("Bearer token retrieved.")]
        public async Task<ActionResult> TestLogin(string email, string password)
        {
            try
            {


                var config = new FirebaseAuthConfig
                {
                    ApiKey = _sm.GetSecret(BCXConstants.GoogleSecrete, BCXConstants.FirebaseSecrete1),
                    AuthDomain = _sm.GetSecret(BCXConstants.GoogleSecrete, BCXConstants.FirebaseSecrete2),
                    Providers = new FirebaseAuthProvider[]
                    {
                        new EmailProvider()
                    }
                };

                var client = new FirebaseAuthClient(config);

                var result = await client.SignInWithEmailAndPasswordAsync(email, password);
                var bearerToken = await result.User.GetIdTokenAsync(true);

                return Ok(new { token = bearerToken });
            }
            catch (Exception)
            {
                return ErrorResponse.BadRequest("Login failed.");
            }
        }
    }
}
