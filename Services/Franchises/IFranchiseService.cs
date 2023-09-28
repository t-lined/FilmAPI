using FilmAPI.Models;

namespace FilmAPI.Services.Franchises
{
    public interface IFranchiseService : ICrudService<Franchise, int>
    {
        Task UpdateMoviesAsync(int franchiseId, int[] movieIds);
        Task<ICollection<Movie>> GetMoviesAsync(int id);
        Task<ICollection<Character>> GetCharactersAsync(int id);
    }
}
