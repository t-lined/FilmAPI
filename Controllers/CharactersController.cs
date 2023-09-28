using AutoMapper;
using FilmAPI.Data.DTOs.Character;
using FilmAPI.Data.Exceptions;
using FilmAPI.Models;
using FilmAPI.Services.Characters;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace FilmAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]

    public class CharactersController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        private readonly IMapper _mapper;

        public CharactersController(ICharacterService characterService, IMapper mapper)
        {
            _characterService = characterService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a list of all characters.
        /// </summary>
        /// <returns>An action result with a collection of character DTOs.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterDTO>>> GetCharacters()
        {
            return Ok(_mapper.Map<IEnumerable<CharacterDTO>>(await _characterService.GetAllAsync()));
        }

        /// <summary>
        /// Gets a character by ID.
        /// </summary>
        /// <param name="id">The ID of the character to retrieve.</param>
        /// <returns>An action result with a character DTO.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested character is not found.</exception>
        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterDTO>> GetCharacter(int id)
        {
            try
            {
                return Ok(_mapper.Map<CharacterDTO>(await _characterService.GetByIdAsync(id)));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing character.
        /// </summary>
        /// <param name="id">The ID of the character to update.</param>
        /// <param name="character">The character data for the update.</param>
        /// <returns>An action result indicating success or failure.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested character is not found.</exception>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacter(int id, CharacterPutDTO character)
        {
            if (id != character.Id)
            {
                return BadRequest();
            }
            try
            {
                var updatedCharacter = await _characterService.UpdateAsync(_mapper.Map<Character>(character));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }

        /// <summary>
        /// Creates a new character.
        /// </summary>
        /// <param name="character">The character data for the creation.</param>
        /// <returns>An action result with the created character DTO.</returns>
        [HttpPost]
        public async Task<ActionResult<CharacterDTO>> PostCharacter(CharacterPostDTO character)
        {
            var newCharacter = await _characterService.AddAsync(_mapper.Map<Character>(character));

            return CreatedAtAction("GetCharacter",
                new { id = newCharacter.Id },
                _mapper.Map<CharacterDTO>(newCharacter));
        }

        /// <summary>
        /// Updates the movies associated with a character.
        /// </summary>
        /// <param name="id">The ID of the character to update movies for.</param>
        /// <param name="movies">An array of movie IDs to associate with the character.</param>
        /// <returns>An action result indicating success or failure.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested character is not found.</exception>
        /// <exception cref="EntityValidationException">Thrown when there is an issue with entity validation.</exception>
        [HttpPut("{id}/movies")]
        public async Task<IActionResult> UpdateMovies(int id, [FromBody] int[] movies)
        {
            try
            {
                await _characterService.UpdateMoviesAsync(id, movies);
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
        /// Deletes a character by ID.
        /// </summary>
        /// <param name="id">The ID of the character to delete.</param>
        /// <returns>An action result indicating success or failure.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested character is not found.</exception>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            try
            {
                await _characterService.DeleteAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }

}
