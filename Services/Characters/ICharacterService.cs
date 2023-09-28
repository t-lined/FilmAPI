using FilmAPI.Models;

namespace FilmAPI.Services.Characters
{
    public interface ICharacterService : ICrudService<Character, int>
    {
        Task UpdateMoviesAsync(int characterId, int[] movieIds);
    }

}