using AutoMapper;
using FilmAPI.Data.DTOs.Character;
using FilmAPI.Data.DTOs.Movie;
using FilmAPI.Data.Exceptions;
using FilmAPI.Models;
using FilmAPI.Services.Movies;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace FilmAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]

    /// <summary>
    /// Controller for managing movie-related operations.
    /// </summary>
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;

        public MoviesController(IMovieService movieService, IMapper mapper)
        {
            _movieService = movieService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a list of all movies.
        /// </summary>
        /// <returns>An action result with a collection of movie DTOs.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies()
        {
            return Ok(_mapper.Map<IEnumerable<MovieDTO>>(await _movieService.GetAllAsync()));
        }

        /// <summary>
        /// Gets the characters associated with a movie by ID.
        /// </summary>
        /// <param name="id">The ID of the movie to retrieve characters for.</param>
        /// <returns>An action result with a collection of character DTOs.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested movie is not found.</exception>
        [HttpGet("{id}/characters")]
        public async Task<ActionResult<IEnumerable<CharacterDTO>>> GetCharacters(int id)
        {
            try
            {
                return Ok(_mapper
                    .Map<IEnumerable<CharacterDTO>>(
                        await _movieService.GetCharactersAsync(id)));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Gets a movie by ID.
        /// </summary>
        /// <param name="id">The ID of the movie to retrieve.</param>
        /// <returns>An action result with a movie DTO.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested movie is not found.</exception>
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDTO>> GetMovie(int id)
        {
            try
            {
                return Ok(_mapper.Map<MovieDTO>(await _movieService.GetByIdAsync(id)));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing movie by ID.
        /// </summary>
        /// <param name="id">The ID of the movie to update.</param>
        /// <param name="movie">The movie data for the update.</param>
        /// <returns>An action result indicating success or failure.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested movie is not found.</exception>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MoviePutDTO movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }
            try
            {
                await _movieService.UpdateAsync(_mapper.Map<Movie>(movie));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }

        /// <summary>
        /// Adds a new movie.
        /// </summary>
        /// <param name="movie">The movie data for the creation.</param>
        /// <returns>An action result with the created movie DTO.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when related entities (e.g., franchise or characters) are not found.</exception>
        [HttpPost]
        public async Task<ActionResult<MovieDTO>> PostMovie(MoviePostDTO movie)
        {
            try
            {
                var newMovie = await _movieService.AddAsync(_mapper.Map<Movie>(movie));

                return CreatedAtAction("GetMovie",
                    new { id = newMovie.Id },
                    _mapper.Map<MovieDTO>(newMovie));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Updates the characters associated with a movie by ID.
        /// </summary>
        /// <param name="id">The ID of the movie to update characters for.</param>
        /// <param name="characterIds">An array of character IDs to associate with the movie.</param>
        /// <returns>An action result indicating success or failure.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested movie is not found.</exception>
        /// <exception cref="EntityValidationException">Thrown when there is an issue with entity validation.</exception>
        [HttpPut("{id}/characters")]
        public async Task<IActionResult> UpdateCharacters(int id, [FromBody] int[] characterIds)
        {
            try
            {
                await _movieService.UpdateCharactersAsync(id, characterIds);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (EntityValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a movie by ID.
        /// </summary>
        /// <param name="id">The ID of the movie to delete.</param>
        /// <returns>An action result indicating success or failure.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested movie is not found.</exception>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            try
            {
                await _movieService.DeleteAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }

}


