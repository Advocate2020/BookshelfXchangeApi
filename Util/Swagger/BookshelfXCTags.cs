using Batsamayi.Shared.API.Swagger;

namespace BookXChangeApi.Util.Swagger
{
    public class BookshelfXCTags : ISwaggerTags
    {
        public const string Auth = "01.Auth";
        public const string Book = "02.Book";
        public const string Category = "03.Category";
        public const string Dev = "04.Dev";
        public const string WebSuperUser = "05.WebSuperUser";

        /// <summary>
        /// All endpoints need to be listed under <see cref="TagNames"/>, in order for them to be sorted alphabetically.
        /// </summary>
        public List<string> TagNames => new()
        {
            Auth,
            Book,
            Category,
            Dev,
            WebSuperUser
        };

        string ISwaggerTags.DeveloperTag => Dev;
    }
}
