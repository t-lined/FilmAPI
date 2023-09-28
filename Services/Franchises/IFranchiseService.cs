using FilmAPI.Models;

namespace FilmAPI.Services.Franchises
{
    /// <summary>
    /// Interface for performing CRUD (Create, Read, Update, Delete) operations on Franchise entities.
    /// </summary>
    public interface IFranchiseService : ICrudService<Franchise, int>
    {
        /// <summary>
        /// Updates the movies associated with a franchise.
        /// </summary>
        /// <param name="franchiseId">The ID of the franchise to update movies for.</param>
        /// <param name="movieIds">An array of movie IDs to associate with the franchise.</param>
        Task UpdateMoviesAsync(int franchiseId, int[] movieIds);

        /// <summary>
        /// Gets the movies associated with a franchise by its ID.
        /// </summary>
        /// <param name="id">The ID of the franchise to retrieve movies for.</param>
        /// <returns>A collection of movies associated with the franchise.</returns>
        Task<ICollection<Movie>> GetMoviesAsync(int id);

        /// <summary>
        /// Gets the characters associated with a franchise by its ID.
        /// </summary>
        /// <param name="id">The ID of the franchise to retrieve characters for.</param>
        /// <returns>A collection of characters associated with the franchise.</returns>
        Task<ICollection<Character>> GetCharactersAsync(int id);
    }

}
