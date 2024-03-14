using BookXChangeDB.Databases.BaseData;
using BookXChangeDB.Models;
using Microsoft.EntityFrameworkCore;

namespace BookXChangeDB.Databases
{
    public partial class DatabaseContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);


            SeedData(modelBuilder);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            AddCategories(modelBuilder);

            AddBooks(modelBuilder);

        }

        private static void AddBooks(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(BookData.data);
        }

        private static void AddCategories(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                CategoryData.data
            );
        }
    }
}
