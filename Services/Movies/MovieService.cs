using FilmAPI.Data.Exceptions;
using FilmAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmAPI.Services.Movies
{
    public class MovieService : IMovieService
    {
        private readonly FilmAPIDbContext _context;

        // Constructor that takes a FilmAPIDbContext as a parameter and initializes the _context field.
        public MovieService(FilmAPIDbContext context)
        {
            _context = context;
        }

        // Method to add a Movie object to the database asynchronously.
        public async Task<Movie> AddAsync(Movie obj)
        {
            // Check if the FranchiseId is not null and if it references an existing Franchise
            if (!await FranchiseExistsAsync(obj.FranchiseId.Value))
            {
                throw new EntityNotFoundException(nameof(Franchise), obj.FranchiseId.Value);
            }

            await _context.Movies.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }


        // Method to delete a Movie from the database asynchronously.
        public async Task DeleteAsync(int id)
        {
            if (!await MovieExistsAsync(id))
                throw new EntityNotFoundException(nameof(Movie), id);

            var movie = await _context.Movies
                .Where(m => m.Id == id)
                .FirstAsync();

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }

        // Method to retrieve all Movie objects from the database asynchronously, including their associated Characters.
        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await _context.Movies.Include(m => m.Characters).ToListAsync();
        }

        // Method to retrieve a collection of Characters associated with a Movie from the database asynchronously.
        public async Task<ICollection<Character>> GetCharactersAsync(int movieId)
        {
            if (!await MovieExistsAsync(movieId))
                throw new EntityNotFoundException(nameof(Movie), movieId);

            var movie = await _context.Movies
                .Include(m => m.Characters)
                .Where(m => m.Id == movieId)
                .FirstOrDefaultAsync();

            return movie.Characters.ToList();
        }

        // Method to retrieve a Movie by its ID from the database asynchronously, including its associated Characters.
        public async Task<Movie> GetByIdAsync(int id)
        {
            if (!await MovieExistsAsync(id))
                throw new EntityNotFoundException(nameof(Movie), id);

            var movie = await _context.Movies.
                Where(m => m.Id == id)
                .Include(m => m.Characters)
                .FirstAsync();

            return movie;
        }

        // Method to update a Movie object in the database asynchronously.
        public async Task<Movie> UpdateAsync(Movie obj)
        {
            if (!await MovieExistsAsync(obj.Id))
                throw new EntityNotFoundException(nameof(Movie), obj.Id);

            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();

            return obj;
        }

        // Method to update the list of Characters associated with a Movie in the database asynchronously.
        public async Task UpdateCharactersAsync(int movieId, int[] characterIds)
        {
            if (!await MovieExistsAsync(movieId))
                throw new EntityNotFoundException(nameof(Movie), movieId);

            var movie = await _context.Movies
                .Include(m => m.Characters)
                .FirstOrDefaultAsync(c => c.Id == movieId);

            movie.Characters.Clear();

            foreach (int id in characterIds)
            {
                if (!await CharacterExistsAsync(id))
                    throw new EntityNotFoundException(nameof(Character), id);

                var character = await _context.Characters.FindAsync(id);
                movie.Characters.Add(character);
            }

            await _context.SaveChangesAsync();

        }

        // Check if a Movie with the given ID exists in the database asynchronously.
        private async Task<bool> MovieExistsAsync(int id)
        {
            return await _context.Movies.AnyAsync(m => m.Id == id);
        }

        // Check if a Character with the given ID exists in the database asynchronously.
        private async Task<bool> CharacterExistsAsync(int id)
        {
            return await _context.Characters.AnyAsync(c => c.Id == id);
        }

        private async Task<bool> FranchiseExistsAsync(int? franchiseId)
        {
            if (franchiseId.HasValue)
            {
                return await _context.Franchises.AnyAsync(f => f.Id == franchiseId);
            }

            return false;
        }

    }

}
