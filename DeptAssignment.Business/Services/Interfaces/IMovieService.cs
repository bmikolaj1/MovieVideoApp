using DeptAssignment.Business.Models.Imdb;
using System.Threading.Tasks;

namespace DeptAssignment.Business.Services.Interfaces
{
    public interface IMovieService
    {
        Task<SearchData> SearchMovies(string searchStr);
    }
}
