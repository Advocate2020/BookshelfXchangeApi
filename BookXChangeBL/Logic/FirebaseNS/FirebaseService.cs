using Batsamayi.Shared.BL.ExceptionHandling;
using Batsamayi.Shared.BL.Strings;
using Firebase.Auth;
using FirebaseAdmin.Auth;

using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BookXChangeBL.Logic.FirebaseNS
{
    public class FirebaseService
    {
        private readonly FirebaseAuth firebaseAuth;
        private readonly FirebaseAuthClient _firebaseAuthClient;
        public FirebaseService()
        {

            // Initialize FirebaseAuth instance
            firebaseAuth = FirebaseAuth.DefaultInstance;
        }

        /// <summary>
        ///     Get the firebase id of the signed in user.
        /// </summary>
        public static async Task<string> GetFirebaseIdAsync(HttpRequest request)
        {
            var token = await GetTokenAsync(request);

            return token.Uid;
        }
        public static async Task<string> GetUsersTokenAsync(HttpRequest request)
        {
            var token = await GetUserTokenAsync(request);

            return token;
        }

        /// <summary>
        /// Get the email stored inside the firebase token.
        /// </summary>

        public static async Task<string> GetUserEmail(string userId)
        {
            UserRecord userRecord = await FirebaseAuth.DefaultInstance.GetUserAsync(userId);

            return userRecord.Email;
        }

        public async Task<string> GetFirebaseIdByEmailAsync(string email)
        {
            try
            {
                // Get the user by email
                var userRecord = await firebaseAuth.GetUserByEmailAsync(email);

                // Return the Firebase UID (ID)
                return userRecord.Uid;
            }
            catch (FirebaseAdmin.Auth.FirebaseAuthException ex)
            {
                // Handle any authentication errors
                Console.WriteLine($"Error getting Firebase user ID: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"Error getting Firebase user ID: {ex.Message}");
                throw;
            }
        }




        public static async Task<FirebaseToken> GetTokenAsync(HttpRequest request)
        {
            const string bearer = "Bearer ";

            // Get the token from the Authorization header.
            var value = request.Headers.Authorization.ToString();

            if (value is null)
            {
                throw new ClientError(ErrorStrings.WasNull("Bearer token"));
            }

            if (value.Length < bearer.Length)
            {
                throw new ClientError("Invalid token.");
            }

            // Remove the "bearer " text.
            var idToken = value["Bearer ".Length..].Trim();

            var token = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);

            return token;
        }

        public static async Task<string> GetUserTokenAsync(HttpRequest request)
        {
            const string bearer = "Bearer ";

            // Get the token from the Authorization header.
            var value = request.Headers.Authorization.ToString();

            if (value is null)
            {
                throw new ClientError(ErrorStrings.WasNull("Bearer token"));
            }

            if (value.Length < bearer.Length)
            {
                throw new ClientError("Invalid token.");
            }

            // Remove the "bearer " text.
            var idToken = value["Bearer ".Length..].Trim();

            await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);

            return idToken;
        }

        public static async Task SetUserClaims(HttpRequest request, UserCustomClaims userClaims)
        {
            var token = await GetTokenAsync(request);

            var claims = new Dictionary<string, object>
            {
                // Add all roles that the user belongs to the the claims.
                { ClaimTypes.Role, userClaims.Roles },

                // Add the user's database user id as a claim.
                { "UserId", userClaims.UserId }
            };

            await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(token.Uid, claims);
        }

        public static async Task SetAminUserClaims(string firebaseUserId, UserCustomClaims userClaims)
        {
            var claims = new Dictionary<string, object>
            {
                // Add all roles that the user belongs to the the claims.
                { ClaimTypes.Role, userClaims.Roles },
            };

            await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(firebaseUserId, claims);
        }
    }

}
