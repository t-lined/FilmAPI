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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FranchiseDTO>>> GetFranchises()
        {
            return Ok(_mapper.Map<IEnumerable<FranchiseDTO>>(await _franchiseService.GetAllAsync()));
        }

        /// <summary>
        /// Gets a franchise by ID.
        /// </summary>
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
