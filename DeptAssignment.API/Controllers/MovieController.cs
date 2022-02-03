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

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet(Name = "SearchMovies")]
        public async Task<SearchData> SearchMovies(string searchStr)
        {
            return await _movieService.SearchMovies(searchStr);
        }

    }
}
