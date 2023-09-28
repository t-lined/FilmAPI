using System.ComponentModel.DataAnnotations;

namespace FilmAPI.Data.DTOs.Character
{
    public class CharacterPostDTO
    {
        public string FullName { get; set; }
        [StringLength(50)]
        public string? Alias { get; set; }
        [StringLength(50)]
        public string Gender { get; set; }
        [StringLength(50)]
        public string PictureUrl { get; set; }
    }
}
