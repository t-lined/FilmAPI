using System.ComponentModel.DataAnnotations;

namespace FilmAPI.Models
{
    public class Character
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string FullName { get; set; }
        [StringLength(50)]
        public string? Alias { get; set; }
        [StringLength(50)]
        public string Gender { get; set; }
        [StringLength(20)]
        public string PictureUrl { get; set; }
        [StringLength(50)]

        public ICollection<Movie>? Movies { get; set; }
    }
}
