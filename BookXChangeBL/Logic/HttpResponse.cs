using Microsoft.AspNetCore.Http;

namespace BookXChangeBL.Logic
{
    public interface IHttpResponse
    {
        IResponseCookies Cookies { get; }
    }

    public class HttpResponseWrapper : IHttpResponse
    {
        private readonly HttpResponse _response;

        public HttpResponseWrapper(HttpResponse response)
        {
            _response = response;
        }

        public IResponseCookies Cookies => _response.Cookies;
    }
}
