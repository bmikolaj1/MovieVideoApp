using DeptAssignment.Business.Models.Imdb;
using DeptAssignment.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeptAssignment.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IConfiguration _configuration;

        public MovieController(IMovieService movieService, IConfiguration configuration)
        {
            _movieService = movieService;
            _configuration = configuration;
        }

        [HttpGet(Name = "SearchMovies")]
        public async Task<SearchData> SearchMovies(string searchStr)
        {
            string? imdbAPIKey = _configuration["ImdbAPI:APIKey"];
            return await _movieService.SearchMovies(searchStr, imdbAPIKey);
        }

    }
}
