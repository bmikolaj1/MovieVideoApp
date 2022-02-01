using DeptAssignment.Business.Models.Imdb;
using DeptAssignment.Business.Services.Interfaces;
using Newtonsoft.Json;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace DeptAssignment.Business.Services
{
    public class MovieService : IMovieService
    {
        //en is a language parameter and in this assignment we always default it to english
        public const string baseUrl = "https://imdb-api.com/en/API/";

        public HttpClient httpClient = new HttpClient();

        public MovieService()
        {

        }

        public async Task<SearchData> SearchMovies(string searchStr, string apiKey)
        {
            string moviePrefix = "SearchMovie";
            string fullUrl = $"{baseUrl}{moviePrefix}/{apiKey}/{searchStr}";

            HttpResponseMessage response = await httpClient.GetAsync(fullUrl);
            response.EnsureSuccessStatusCode();

            string result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<SearchData>(result);
        }
    }
}
