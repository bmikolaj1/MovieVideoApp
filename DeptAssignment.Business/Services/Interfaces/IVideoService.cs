using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeptAssignment.Business.Services.Interfaces
{
    public interface IVideoService
    {
        Task<string> GetMovieTrailer(string searchStr);
    }
}
