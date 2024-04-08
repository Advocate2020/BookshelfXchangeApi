using Batsamayi.Shared.BL.ExceptionHandling;

namespace BookXChangeApi.Util
{
    internal static class FileUploadValidator
    {
        public const long MaxFileSizeInBytes = 1024 * 1024 * 8; // Max file size allowed for uploaded files.

        /// <summary>
        /// This function will throw an exception of type <see cref="ClientError"/> if validation fails.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="allowedExtensions"></param>
        /// <exception cref="ClientError"></exception>
        internal static void ValidateFile(IFormFile file, params string[] allowedExtensions)
        {
            if (file is null || file.Length == 0)
            {
                throw new ClientError("File is empty.");
            }

            if (file.Length > (MaxFileSizeInBytes))
            {
                throw new ClientError("File size exceeds the maximum allowed size (4 MB).");
            }

            var extension = Path.GetExtension(file.FileName);

            var extensionAllowed = allowedExtensions.Contains(extension);

            if (!extensionAllowed)
            {
                var _extentions = string.Join(" ", allowedExtensions);
                throw new ClientError($"Incorrect file format. Allowed formats: {_extentions}");
            }
        }
    }

    public class FileExtensions
    {
        public const string Jpeg = ".jpeg";
        public const string Jpg = ".jpg";
        public const string Png = ".png";

        public static string[] All => new[] {
            Png,
            Jpg,
            Jpeg
        };
    }
}
