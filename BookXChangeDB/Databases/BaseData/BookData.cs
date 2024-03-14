using BookXChangeDB.Models;


namespace BookXChangeDB.Databases.BaseData
{
    public class BookData
    {
        public static List<Book> data = new()
        {
            new Book() { Id = 1, Title = "Book 1", Author = "Author 1", CategoryId = 1 },
                new Book() { Id = 2, Title = "Book 2", Author = "Author 2", CategoryId = 1 },
                new Book() { Id = 3, Title = "Book 3", Author = "Author 3", CategoryId = 2 },
                new Book() { Id = 4, Title = "Book 4", Author = "Author 4", CategoryId = 2 },
                new Book() { Id = 5, Title = "Book 5", Author = "Author 5", CategoryId = 3 }
        };
    }
}
