using DeptAssignment.Business.Models.Imdb;
using DeptAssignment.Business.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DeptAssignment.Business.Services
{
    public class MovieService : IMovieService
    {

        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;
        public HttpClient httpClient = new HttpClient();

        public MovieService(IConfiguration configuration, IMemoryCache memoryCache)
        {
            _configuration = configuration;
            _memoryCache = memoryCache;
        }

        public async Task<SearchData> SearchMovies(string searchStr)
        {
            string result = string.Empty;
            string moviePrefix = "SearchMovie";
            string imdbAPIKey = _configuration["ImdbAPI:APIKey"];
            string baseUrl = _configuration["ImdbAPI:BaseUrl"];
            string fullUrl = $"{baseUrl}{moviePrefix}/{imdbAPIKey}/{searchStr}";


            // If found in cache, return cached data
            if (_memoryCache.TryGetValue(searchStr, out result))
            {
                return JsonConvert.DeserializeObject<SearchData>(result);
            }

            // if not found, then execute request 
            HttpResponseMessage response = await httpClient.GetAsync(fullUrl);
            response.EnsureSuccessStatusCode();

            result = await response.Content.ReadAsStringAsync();

            // sliding expiration means that it would expire only if it was not accessed in the configured timespan 
            // as oposed to absolute
            MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                                                                    .SetSize(1)
                                                                    .SetSlidingExpiration(TimeSpan.FromSeconds(100))
                                                                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(300));
            // Set object in cache
            _memoryCache.Set(searchStr, result, cacheOptions);

            return JsonConvert.DeserializeObject<SearchData>(result);

        }
    }
}
