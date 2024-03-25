using Batsamayi.Shared.BL.Queries;
using BookXChangeDB.Databases;
using BookXChangeDB.Models;

namespace BookXChangeBL.Logic.UserNS
{
    internal class UserQueries : Queries<DatabaseContext>
    {
        public UserQueries(DatabaseContext context) : base(context)
        {
        }

        internal IQueryable<User> GetUserById(int id)
        {
            return _context.User
               .Where(b => b.Id == id);
        }

        internal IQueryable<User> GetUserByFirebaseId(string firebaseId)
        {
            return _context.User
                .Where(u => u.FirebaseId == firebaseId);
        }

    }
}
