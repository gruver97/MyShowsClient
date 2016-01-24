using Windows.Web.Http;

namespace MysShowsClient.Services.MyShow
{
    public class ErrorData
    {
        public ErrorData(HttpStatusCode statusCode, bool isSearchError)
        {
            StatusCode = statusCode;
            IsSearchError = isSearchError;
        }

        public HttpStatusCode StatusCode { get; set; }

        public bool IsSearchError { get; set; }
    }
}