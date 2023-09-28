using AutoMapper;
using FilmAPI.Data.DTOs.Character;
using FilmAPI.Data.DTOs.Franchise;
using FilmAPI.Data.DTOs.Movie;
using FilmAPI.Data.Exceptions;
using FilmAPI.Models;
using FilmAPI.Services.Franchises;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace FilmAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    /// <summary>
    /// Controller for managing franchise-related operations.
    /// </summary>
    public class FranchiseController : ControllerBase
    {
        private readonly IFranchiseService _franchiseService;
        private readonly IMapper _mapper;

        public FranchiseController(IFranchiseService franchiseService, IMapper mapper)
        {
            _franchiseService = franchiseService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a list of all franchises.
        /// </summary>
        /// <returns>An action result with a collection of franchise DTOs.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FranchiseDTO>>> GetFranchises()
        {
            return Ok(_mapper.Map<IEnumerable<FranchiseDTO>>(await _franchiseService.GetAllAsync()));
        }

        /// <summary>
        /// Gets a franchise by ID.
        /// </summary>
        /// <param name="id">The ID of the franchise to retrieve.</param>
        /// <returns>An action result with a franchise DTO.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested franchise is not found.</exception>
        [HttpGet("{id}")]
        public async Task<ActionResult<FranchiseDTO>> GetFranchise(int id)
        {
            try
            {
                return Ok(_mapper.Map<FranchiseDTO>(await _franchiseService.GetByIdAsync(id)));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Gets the movies associated with a franchise by ID.
        /// </summary>
        /// <param name="id">The ID of the franchise to retrieve movies for.</param>
        /// <returns>An action result with a collection of movie DTOs.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested franchise is not found.</exception>
        [HttpGet("{id}/movies")]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies(int id)
        {
            try
            {
                return Ok(_mapper
                    .Map<IEnumerable<MovieDTO>>(
                        await _franchiseService.GetMoviesAsync(id)));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Gets the characters associated with a franchise by ID.
        /// </summary>
        /// <param name="id">The ID of the franchise to retrieve characters for.</param>
        /// <returns>An action result with a collection of character DTOs.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested franchise is not found.</exception>
        [HttpGet("{id}/characters")]
        public async Task<ActionResult<IEnumerable<CharacterDTO>>> GetCharacters(int id)
        {
            try
            {
                return Ok(_mapper
                    .Map<IEnumerable<CharacterDTO>>(
                        await _franchiseService.GetCharactersAsync(id)));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing franchise by ID.
        /// </summary>
        /// <param name="id">The ID of the franchise to update.</param>
        /// <param name="franchise">The franchise data for the update.</param>
        /// <returns>An action result indicating success or failure.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested franchise is not found.</exception>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFranchise(int id, FranchisePutDTO franchise)
        {
            if (id != franchise.Id)
            {
                return BadRequest();
            }
            try
            {
                await _franchiseService.UpdateAsync(_mapper.Map<Franchise>(franchise));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }

        /// <summary>
        /// Updates the movies associated with a franchise by ID.
        /// </summary>
        /// <param name="id">The ID of the franchise to update movies for.</param>
        /// <param name="movies">An array of movie IDs to associate with the franchise.</param>
        /// <returns>An action result indicating success or failure.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested franchise is not found.</exception>
        /// <exception cref="EntityValidationException">Thrown when there is an issue with entity validation.</exception>
        [HttpPut("{id}/movies")]
        public async Task<IActionResult> UpdateMovies(int id, [FromBody] int[] movies)
        {
            try
            {
                await _franchiseService.UpdateMoviesAsync(id, movies);
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
        /// Adds a new franchise.
        /// </summary>
        /// <param name="franchise">The franchise data for the creation.</param>
        /// <returns>An action result with the created franchise DTO.</returns>
        [HttpPost]
        public async Task<ActionResult<FranchiseDTO>> PostFranchise(FranchisePostDTO franchise)
        {
            var newFranchise = await _franchiseService.AddAsync(_mapper.Map<Franchise>(franchise));

            return CreatedAtAction("GetFranchise",
                new { id = newFranchise.Id },
                _mapper.Map<FranchiseDTO>(newFranchise));
        }

        /// <summary>
        /// Deletes a franchise by ID.
        /// </summary>
        /// <param name="id">The ID of the franchise to delete.</param>
        /// <returns>An action result indicating success or failure.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested franchise is not found.</exception>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFranchise(int id)
        {
            try
            {
                await _franchiseService.DeleteAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }

}
