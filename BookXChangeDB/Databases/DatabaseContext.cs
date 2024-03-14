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


    }
}
