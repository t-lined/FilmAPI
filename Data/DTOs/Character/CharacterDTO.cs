using FilmAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace FilmAPI.Data.DTOs.Character
{
    public class CharacterDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string? Alias { get; set; }
        public string Gender { get; set; }

        public string PictureUrl { get; set; }

        public int[] Movies { get; set; } // Navigation property
    }
}

