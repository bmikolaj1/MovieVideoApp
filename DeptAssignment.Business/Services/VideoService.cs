using DeptAssignment.Business.Services.Interfaces;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DeptAssignment.Business.Services
{
    public class VideoService : IVideoService
    {
        private readonly IConfiguration _configuration;
        public VideoService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GetMovieTrailer(string searchStr)
        {
            string apiKey = _configuration["YoutubeAPI:APIKey"];
            YouTubeService youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = GetType().ToString()
            });

            SearchResource.ListRequest searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = $"{searchStr}-trailer";
            searchListRequest.MaxResults = 1;
            searchListRequest.Type = "video";

            Google.Apis.YouTube.v3.Data.SearchListResponse searchListResponse = await searchListRequest.ExecuteAsync();


            if (searchListResponse is null)
            {
                return string.Empty;
            }

            return searchListResponse.Items.FirstOrDefault()?.Id?.VideoId;
        }
    }
}
