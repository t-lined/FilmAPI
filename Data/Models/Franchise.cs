using System.ComponentModel.DataAnnotations;

namespace FilmAPI.Models
{
    public class Franchise
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Description { get; set; }

        public ICollection<Movie>? Movies { get; set; }
    }
}
