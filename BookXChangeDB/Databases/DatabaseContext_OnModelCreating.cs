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
            //AddCategories(modelBuilder);

            //AddBooks(modelBuilder);
            AddRoles(modelBuilder);

        }

        private static void AddBooks(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(BookData.data);
        }

        private static void AddRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(RoleData.data);
        }

        private static void AddCategories(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                CategoryData.data
            );
        }
    }

}
