using BookXChangeDB.Models;

namespace BookXChangeDB.Databases.BaseData
{
    public static class RoleData
    {

        public static List<Role> data = new()
        {
            USER,ADMIN
        };

        public static Role ADMIN => new()
        {
            Id = 1,
            Name = "Admin",
            Description = "Administrator role"
        };

        public static Role USER => new()
        {
            Id = 2,
            Name = "User",
            Description = "Regular user role"
        };
    }
}
