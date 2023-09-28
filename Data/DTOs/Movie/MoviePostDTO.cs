using System.ComponentModel.DataAnnotations;

namespace FilmAPI.Data.DTOs.Movie
{
    public class MoviePostDTO
    {
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(50)]
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
        [StringLength(50)]
        public string Director { get; set; }
        [StringLength(100)]
        public string PictureUrl { get; set; }
        [StringLength(100)]
        public string TrailerUrl { get; set; }

        public int FranchiseId { get; set; } // Foreign Key
        //public Franchise Franchise { get; set; } // Navigation property
        //public int[] Characters { get; set; } // Navigation property
    }
}
