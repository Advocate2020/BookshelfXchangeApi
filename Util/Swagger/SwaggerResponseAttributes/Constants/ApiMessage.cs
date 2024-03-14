namespace BookXChangeApi.Util.Swagger.SwaggerResponseAttributes.Constants
{
    public static class ApiMessage
    {
        public const string Unauthorized = "Unauthorized. Invalid JWT Token or API Key.";
        public const string Forbidden = "Forbidden. The user does not have permission to access this endpoint.";
        public const string BadRequest = "Bad request. Please check the response for more information.";
        public const string UnhandledException = "Exception. Something went wrong.";
    }
}