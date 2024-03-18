using BookXChangeDB.Models;

namespace BookXChangeDB.Databases.BaseData
{
    public class RoleData
    {
        public static List<Role> data = new()
        {
            new Role {Id = 1, Name = "Admin", Description = "Administrator role" },
            new Role {Id = 2, Name = "User", Description = "Regular user role" }
        };
    }
}
