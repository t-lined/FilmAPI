using FilmAPI.Models;

namespace FilmAPI.Services.Characters
{
    /// <summary>
    /// Interface for performing CRUD (Create, Read, Update, Delete) operations on Character entities.
    /// </summary>
    public interface ICharacterService : ICrudService<Character, int>
    {
        /// <summary>
        /// Updates the movies associated with a character.
        /// </summary>
        /// <param name="characterId">The ID of the character to update movies for.</param>
        /// <param name="movieIds">An array of movie IDs to associate with the character.</param>
        Task UpdateMoviesAsync(int characterId, int[] movieIds);
    }


}