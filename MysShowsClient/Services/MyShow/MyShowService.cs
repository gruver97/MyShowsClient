using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Filters;
using Windows.Web.Http.Headers;
using Microsoft.Practices.Unity;
using MysShowsClient.Model;
using MysShowsClient.Model.Parser;

namespace MysShowsClient.Services.MyShow
{
    public class MyShowService : IMyShowService
    {
        private const string MyShowsApiBaseAddress = "http://api.myshows.ru";
        private const string SearchPart = "/shows/search/?q={0}";
        private const string InfoEpisodesPart = "/shows/{0}";
        private readonly HttpClient _httpClient;
        private readonly IParser _parser;

        public MyShowService([Dependency] IParser parser)
        {
            _parser = parser;
            var protocolFilter = new HttpBaseProtocolFilter {AutomaticDecompression = true};
            _httpClient = new HttpClient(protocolFilter);
            _httpClient.DefaultRequestHeaders.Accept.Add(new HttpMediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new HttpContentCodingWithQualityHeaderValue("utf-8"));
        }

        public async Task<Tuple<IEnumerable<ShortDescription>, ErrorData>> SearchShowsAsync(string searchQuery)
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
                throw new ArgumentException("Argument is null or whitespace", nameof(searchQuery));
            try
            {
                var baseUri = new Uri(MyShowsApiBaseAddress);
                var address = new Uri(baseUri, string.Format(SearchPart, searchQuery));
                var response = await _httpClient.GetAsync(address);
                if (!response.IsSuccessStatusCode)
                {
                    ErrorData errorData = null;
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            errorData = new ErrorData(response.StatusCode, true);
                            break;
                        case HttpStatusCode.InternalServerError:
                            errorData = new ErrorData(response.StatusCode, true);
                            break;
                        default:
                            errorData = new ErrorData(response.StatusCode, false);
                            break;
                    }
                    return new Tuple<IEnumerable<ShortDescription>, ErrorData>(null, errorData);
                }
                var jsonString = await response.Content.ReadAsStringAsync();
                var parsedResponse = await _parser.DeserializeShortDescriptionAsync(jsonString).ConfigureAwait(false);
                //вместо 404 сервер возвращает пустой массив - он будет считаться за ответ 404!
                if (parsedResponse != null)
                {
                    if (!parsedResponse.Any())
                    {
                        return new Tuple<IEnumerable<ShortDescription>, ErrorData>(parsedResponse,
                            new ErrorData(HttpStatusCode.NotFound, true));
                    }
                }
                return new Tuple<IEnumerable<ShortDescription>, ErrorData>(parsedResponse, null);
            }
            catch (ArgumentException)
            {
                return new Tuple<IEnumerable<ShortDescription>, ErrorData>(null,
                    new ErrorData(HttpStatusCode.None, false));
            }
        }

        public async Task<Tuple<ExtendedDescription, ErrorData>> GetShowDescriptionAsync(int showId)
        {
            try
            {
                var baseUri = new Uri(MyShowsApiBaseAddress);
                var address = new Uri(baseUri, string.Format(InfoEpisodesPart, showId));
                var response = await _httpClient.GetAsync(address);
                if (!response.IsSuccessStatusCode)
                {
                    ErrorData errorData = null;
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            errorData = new ErrorData(response.StatusCode, true);
                            break;
                        case HttpStatusCode.InternalServerError:
                            errorData = new ErrorData(response.StatusCode, true);
                            break;
                        default:
                            errorData = new ErrorData(response.StatusCode, false);
                            break;
                    }
                    return new Tuple<ExtendedDescription, ErrorData>(null, errorData);
                }
                var jsonString = await response.Content.ReadAsStringAsync();
                var parsedResponse = await _parser.DeserializeExtendedDescriptionAsync(jsonString).ConfigureAwait(false);
                return parsedResponse == null
                    ? new Tuple<ExtendedDescription, ErrorData>(null, new ErrorData(HttpStatusCode.None, false))
                    : new Tuple<ExtendedDescription, ErrorData>(parsedResponse, null);
            }
            catch (ArgumentException)
            {
                return new Tuple<ExtendedDescription, ErrorData>(null,
                    new ErrorData(HttpStatusCode.None, false));
            }
        }
    }
}