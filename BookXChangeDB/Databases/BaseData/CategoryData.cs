using BookXChangeDB.Models;

namespace BookXChangeDB.Databases.BaseData
{
    public class CategoryData
    {
        public static List<Category> data = new()
        {
            new Category { Id = 1, Name = "Fiction" },
            new Category { Id = 2, Name = "Non-Fiction" },
            new Category { Id = 3, Name = "Science Fiction" }
        };
    }
}
