using BookXChangeDB.Models;

namespace BookXChangeBL.Logic.FirebaseNS
{
    public class UserCustomClaims
    {
        public UserCustomClaims(int userId, params Role[] roles)
        {
            UserId = userId;

            // Get and store the role names.
            Roles = roles.Select(r => r.Name).ToList();
        }

        /// <summary>
        ///     The roles that the user has.
        /// </summary>
        public List<string> Roles { get; }

        public int UserId { get; }
    }
}
