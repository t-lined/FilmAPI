using System.ComponentModel.DataAnnotations;

namespace FilmAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(50)]
        public string Genre { get; set; }
        [StringLength(10)]
        public int ReleaseYear { get; set; }
        [StringLength(50)]
        public string Director { get; set; }
        [StringLength(100)]
        public string PictureUrl { get; set; }
        [StringLength(100)]
        public string TrailerUrl { get; set; }

        public int? FranchiseId { get; set; } // Foreign Key
        public Franchise Franchise { get; set; } // Navigation property
        public ICollection<Character>? Characters { get; set; } // Navigation property
    }
}

