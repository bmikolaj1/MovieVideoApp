using DeptAssignment.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeptAssignment.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideoController : ControllerBase
    {
        private readonly IVideoService _videoService;

        public VideoController(IVideoService videoService)
        {
            _videoService = videoService;
        }

        [HttpGet(Name = "GetMovieTrailer")]
        public async Task<string> GetMovieTrailer(string searchStr)
        {
            return await _videoService.GetMovieTrailer(searchStr);
        }
    }
}


