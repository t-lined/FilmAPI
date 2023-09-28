using FilmAPI.Data.Exceptions;
using FilmAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmAPI.Services.Characters
{
    public class CharacterService : ICharacterService
    {
        private readonly FilmAPIDbContext _context;

        public CharacterService(FilmAPIDbContext context)
        {
            _context = context;
        }

        // Method to add a Character object to the database asynchronously
        public async Task<Character> AddAsync(Character obj)
        {
            await _context.Characters.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        // Method to retrieve all Character objects from the database asynchronously, including their associated Movies
        public async Task<IEnumerable<Character>> GetAllAsync()
        {
            return await _context.Characters.Include(c => c.Movies).ToListAsync();
        }

        // Method to retrieve a Character by its ID from the database asynchronously, including its associated Movies
        public async Task<Character> GetByIdAsync(int id)
        {
            if (!await CharacterExistsAsync(id))
                throw new EntityNotFoundException(nameof(Character), id);

            var character = await _context.Characters.Where(c => c.Id == id)
                .Include(c => c.Movies)
                .FirstAsync();

            return character;
        }

        // Method to update a Character object in the database asynchronously
        public async Task<Character> UpdateAsync(Character obj)
        {
            if (!await CharacterExistsAsync(obj.Id))
                throw new EntityNotFoundException(nameof(Character), obj.Id);

            // Mark the object as modified so it will be updated in the database
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();

            return obj;
        }

        public async Task UpdateMoviesAsync(int characterId, int[] movieIds)
        {
            if (movieIds.Length > 5)
                throw new EntityValidationException("A Character can only have 5 movies.");

            if (!await CharacterExistsAsync(characterId))
                throw new EntityNotFoundException(nameof(Character), characterId);

            var character = await _context.Characters
             .Include(c => c.Movies)
             .FirstOrDefaultAsync(c => c.Id == characterId);

            character.Movies.Clear();

            foreach (int id in movieIds)
            {
                if (!await MovieExistsAsync(id))
                    throw new EntityNotFoundException(nameof(Movie), id);

                var movie = await _context.Movies.FindAsync(id);
                character.Movies.Add(movie);
            }
            await _context.SaveChangesAsync();
        }

        // Method to delete a Character from the database asynchronously
        public async Task DeleteAsync(int id)
        {
            if (!await CharacterExistsAsync(id))
                throw new EntityNotFoundException(nameof(Character), id);

            var character = await _context.Characters
                .Where(c => c.Id == id)
                .FirstAsync();

            // Clear the Movies associated with the Character and then remove the Character
            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
        }

        // Check if a Character with the given ID exists in the database asynchronously
        private async Task<bool> CharacterExistsAsync(int id)
        {
            return await _context.Characters.AnyAsync(c => c.Id == id);
        }

        // Check if a Movie with the given ID exists in the database asynchronously
        private async Task<bool> MovieExistsAsync(int movieId)
        {
            return await _context.Movies.AnyAsync(m => m.Id == movieId);
        }

        // Helper method to throw an exception when a Character has too many Movies
        private static void FailWithTooManyMovies()
        {
            throw new EntityValidationException("Character has too many movies");
        }



    }

}

