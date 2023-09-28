using FilmAPI.Models;

namespace FilmAPI.Services.Movies
{
    /// <summary>
    /// Interface for performing CRUD (Create, Read, Update, Delete) operations on Movie entities.
    /// </summary>
    public interface IMovieService : ICrudService<Movie, int>
    {
        /// <summary>
        /// Updates the characters associated with a movie.
        /// </summary>
        /// <param name="movieId">The ID of the movie to update characters for.</param>
        /// <param name="characterIds">An array of character IDs to associate with the movie.</param>
        Task UpdateCharactersAsync(int movieId, int[] characterIds);

        /// <summary>
        /// Gets the characters associated with a movie by its ID.
        /// </summary>
        /// <param name="id">The ID of the movie to retrieve characters for.</param>
        /// <returns>A collection of characters associated with the movie.</returns>
        Task<ICollection<Character>> GetCharactersAsync(int id);
    }

}
