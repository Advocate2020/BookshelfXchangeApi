using Batsamayi.Shared.BL.BusinessLayerNS;
using Batsamayi.Shared.BL.Extensions;
using BookXChangeBL.DTOs.POST;
using BookXChangeBL.Logic.FirebaseNS;
using BookXChangeDB.Databases;
using BookXChangeDB.Databases.BaseData;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace BookXChangeBL.Logic.UserNS
{
    public class UserBL : BL<DatabaseContext>
    {
        private readonly UserQueries _Queries;
        private readonly FirebaseService _Auth;
        private readonly HttpContext httpContext;

        public UserBL(IDbContextFactory<DatabaseContext> contextFactory, DatabaseContext context) : base(contextFactory, context)
        {
            _Queries = new UserQueries(context);
            _Auth = new FirebaseService();

        }

        public async Task AddUserAsync(AddUserDTO form, string firebaseId, HttpRequest request, string userEmail)
        {
            int userId = 0;

            await ContextFactory
                .CreateDbContextAsync()
                .WithDefaultTimeout()
                .ExecuteWithTransaction(async (tContext) =>
                {
                    await FlagUserAlreadyExists(firebaseId);

                    // Add a new user, along with their profile.
                    var user = form.Map(firebaseId, userEmail);
                    tContext.Add(user);

                    await tContext.SaveChangesAsync();

                    userId = user.Id;
                });

            await FirebaseAddInitialClaimsAsync(request, userId); // Store the user role in their firebase token claims. This should be done after the transaction is committed to ensure that the user was successfully created.
        }

        private async Task FlagUserAlreadyExists(string firebaseId)
        {
            await _Queries
                .GetUserByFirebaseId(firebaseId)
                .AnyAsync()
                .FailIfTrueAsync("Profile already exists.");
        }



        private static async Task FirebaseAddInitialClaimsAsync(HttpRequest request, int userId)
        {
            var customClaims = new UserCustomClaims(userId, RoleData.USER);
            await FirebaseService.SetUserClaims(request, customClaims);
        }
    }
}
