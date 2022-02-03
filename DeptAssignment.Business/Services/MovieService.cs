using DeptAssignment.Business.Models.Imdb;
using DeptAssignment.Business.Services.Interfaces;
using DeptAssignment.Common.Common.InMemCache;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace DeptAssignment.Business.Services
{
    public class MovieService : IMovieService
    {

        private readonly IConfiguration _configuration;

        public HttpClient httpClient = new HttpClient();


        public MovieService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<SearchData> SearchMovies(string searchStr)
        {
            string moviePrefix = "SearchMovie";
            string imdbAPIKey = _configuration["ImdbAPI:APIKey"];
            string baseUrl = _configuration["ImdbAPI:BaseUrl"];
            string fullUrl = $"{baseUrl}{moviePrefix}/{imdbAPIKey}/{searchStr}";

            InMemCache<HttpResponseMessage> movieCache = new InMemCache<HttpResponseMessage>();

            //HttpResponseMessage response = await httpClient.GetAsync(fullUrl);
            //response.EnsureSuccessStatusCode();

            //check if the same movies with the exact searchStr exist in cache, if not execute request
            HttpResponseMessage movieResult = await movieCache.GetOrCreate(searchStr, async () => await httpClient.GetAsync(fullUrl));

            string result = await movieResult.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<SearchData>(result);
        }
    }
}
