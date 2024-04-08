using BookXChangeApi.Util.ResponseNS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BookXChangeApi.Util
{
    public enum ErrorResponseType
    {
        Error,
        Exception,
        NotImplemented
    }
    public class ErrorResponse
    {
        public static ActionResult BadRequest(string message)
        {
            return new OkObjectResult(new ErrorResponseBody<object>(message, ErrorResponseType.Error))
            {
                StatusCode = 400
            };
        }

        public static ActionResult CustomError(string message, [ActionResultStatusCode] int statusCode)
        {
            return new ObjectResult(new ErrorResponseBody<object>(message, ErrorResponseType.Error))
            {
                StatusCode = statusCode
            };
        }

        public static ActionResult Exception(string debugMessage)
        {
            return new ObjectResult(new ErrorResponseBody<object>("An error occurred.", ErrorResponseType.Exception)
            {
                DebugMessage = debugMessage
            })
            {
                StatusCode = 500
            };
        }

        public static ActionResult NotImplemented(string feature)
        {
            return new ObjectResult(new ErrorResponseBody<object>(feature + " has not been implemented.", ErrorResponseType.NotImplemented))
            {
                StatusCode = 500
            };
        }
    }
}
