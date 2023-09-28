namespace FilmAPI.Data.DTOs.Movie
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
        public string Director { get; set; }
        public string PictureUrl { get; set; }
        public string TrailerUrl { get; set; }

        public int? FranchiseId { get; set; } // Foreign Key
        //public Franchise Franchise { get; set; } // Navigation property
        public int[]? Characters { get; set; } // Navigation property
    }
}
