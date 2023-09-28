using FilmAPI.Models;

namespace FilmAPI.Services.Movies
{
    public interface IMovieService : ICrudService<Movie, int>
    {
        Task UpdateCharactersAsync(int movieId, int[] characterIds);
        Task<ICollection<Character>> GetCharactersAsync(int id);
    }
}
