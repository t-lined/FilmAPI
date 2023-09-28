using FilmAPI.Data.Exceptions;
using FilmAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmAPI.Services.Franchises
{
    public class FranchiseService : IFranchiseService
    {
        private readonly FilmAPIDbContext _context;
        public FranchiseService(FilmAPIDbContext context)
        {
            _context = context;
        }

        // Method to add a Franchise object to the database asynchronously
        public async Task<Franchise> AddAsync(Franchise obj)
        {
            await _context.Franchises.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        // Method to retrieve all Franchise objects from the database asynchronously, including their associated Movies
        public async Task<IEnumerable<Franchise>> GetAllAsync()
        {
            return await _context.Franchises.Include(f => f.Movies).ToListAsync();
        }

        // Method to retrieve a Franchise by its ID from the database asynchronously, including its associated Movies
        public async Task<Franchise> GetByIdAsync(int id)
        {
            var franchise = await _context.Franchises.Where(f => f.Id == id)
                .Include(f => f.Movies)
                .FirstOrDefaultAsync();

            if (franchise is null)
                throw new EntityNotFoundException(nameof(Franchise), id);

            return franchise;
        }

        // Method to retrieve a collection of Movies associated with a Franchise from the database asynchronously
        public async Task<ICollection<Movie>> GetMoviesAsync(int id)
        {
            if (!await FranchiseExistsAsync(id))
                throw new EntityNotFoundException(nameof(Franchise), id);

            return await _context.Movies
                .Where(m => m.FranchiseId == id)
                .ToListAsync();
        }

        // Method to retrieve a collection of Characters associated with a Franchise from the database asynchronously
        public async Task<ICollection<Character>> GetCharactersAsync(int id)
        {
            if (!await FranchiseExistsAsync(id))
                throw new EntityNotFoundException(nameof(Franchise), id);

            var charactersInFranchise = await _context.Characters
                .Where(c => c.Movies.Any(m => m.FranchiseId == id))
                .ToListAsync();

            return charactersInFranchise;
        }

        // Method to update a Franchise object in the database asynchronously
        public async Task<Franchise> UpdateAsync(Franchise obj)
        {
            if (!await FranchiseExistsAsync(obj.Id))
                throw new EntityNotFoundException(nameof(Franchise), obj.Id);

            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();

            return obj;
        }

        // Method to update the list of Movies associated with a Franchise in the database asynchronously
        public async Task UpdateMoviesAsync(int franchiseId, int[] movieIds)
        {
            var franchise = await _context.Franchises
                .Include(c => c.Movies)
                .FirstOrDefaultAsync(c => c.Id == franchiseId);

            if (franchise != null)
            {
                // Clear the existing Movies associated with the Franchise
                franchise.Movies.Clear();

                foreach (int id in movieIds)
                {
                    if (!await MovieExistsAsync(id))
                        throw new EntityNotFoundException(nameof(Movie), id);

                    var movie = await _context.Movies.FindAsync(id);
                    franchise.Movies.Add(movie);
                }

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new EntityNotFoundException(nameof(Franchise), franchiseId);
            }
        }

        // Method to delete a Franchise from the database asynchronously
        public async Task DeleteAsync(int id)
        {
            if (!await FranchiseExistsAsync(id))
                throw new EntityNotFoundException(nameof(Franchise), id);

            var franchise = await _context.Franchises
                .Include(f => f.Movies)
                .FirstOrDefaultAsync(f => f.Id == id);

            franchise.Movies.Clear();
            _context.Franchises.Remove(franchise);
            await _context.SaveChangesAsync();
        }

        // Check if a Franchise with the given ID exists in the database asynchronously
        private async Task<bool> FranchiseExistsAsync(int id)
        {
            return await _context.Franchises.AnyAsync(f => f.Id == id);
        }

        // Check if a Movie with the given ID exists in the database asynchronously
        private async Task<bool> MovieExistsAsync(int movieId)
        {
            return await _context.Movies.AnyAsync(m => m.Id == movieId);
        }

        // Helper method to throw an exception when a Franchise has too many Movies
        private static void FailWithTooManyMovies()
        {
            throw new EntityValidationException("Franchise has too many movies");
        }
    }

}
