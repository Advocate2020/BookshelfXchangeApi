using BookXChangeDB.Models;
using Microsoft.EntityFrameworkCore;


namespace BookXChangeDB.Databases
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Person> Person { get; set; }

    }
}
