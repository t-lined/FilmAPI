using Microsoft.EntityFrameworkCore;

namespace FilmAPI.Models
{
    public class FilmAPIDbContext : DbContext
    {
        public FilmAPIDbContext()
        {
        }

        public FilmAPIDbContext(DbContextOptions<FilmAPIDbContext> options)
            : base(options)
        {
        }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Franchise> Franchises { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            optionsBuilder.UseSqlServer("Data Source = N-NO-01-04-6995\\SQLEXPRESS; Initial Catalog = FilmAPI; Integrated Security = true; Trust Server Certificate = true;");
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            // Seed data for Character entity
            modelBuilder.Entity<Character>()
                .HasData(
                    new Character { Id = 1, FullName = "John Smith", Alias = "Captain Hero", Gender = "Male", PictureUrl = "john_smith.jpg" },
                    new Character { Id = 2, FullName = "Jane Doe", Alias = "Wonder Woman", Gender = "Female", PictureUrl = "jane_doe.jpg" },
                    new Character { Id = 3, FullName = "David Johnson", Alias = "Spider-Man", Gender = "Male", PictureUrl = "david_johnson.jpg" }
                );

            // Seed data for Franchise entity
            modelBuilder.Entity<Franchise>()
                .HasData(
                    new Franchise { Id = 1, Name = "Marvel Cinematic Universe", Description = "A series of interconnected superhero films" },
                    new Franchise { Id = 2, Name = "Wonder Woman", Description = "A superhero film series based on DC Comics" }
                );

            // Seed data for Movie entity
            modelBuilder.Entity<Movie>()
                .HasData(
                    new Movie { Id = 1, Title = "Avengers: Endgame", Genre = "Action, Adventure", ReleaseYear = 2019, Director = "Anthony and Joe Russo", PictureUrl = "avengers_endgame.jpg", TrailerUrl = "https://www.youtube.com/watch?v=TcMBFSGVi1c", FranchiseId = 1 },
                    new Movie { Id = 2, Title = "Wonder Woman 1984", Genre = "Action, Adventure", ReleaseYear = 2020, Director = "Patty Jenkins", PictureUrl = "wonder_woman_1984.jpg", TrailerUrl = "https://www.youtube.com/watch?v=sfM7_JLk-84", FranchiseId = 2 },
                    new Movie { Id = 3, Title = "Spider-Man: No Way Home", Genre = "Action, Adventure, Sci-Fi", ReleaseYear = 2021, Director = "Jon Watts", PictureUrl = "spiderman_no_way_home.jpg", TrailerUrl = "https://www.youtube.com/watch?v=g4Hbz2jLxvQ", FranchiseId = 1 }
                );

            modelBuilder.Entity<Character>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Movie>()
                .HasKey(m => m.Id);


            // Configure many-to-many relationship between Character and Movie
            modelBuilder.Entity<Character>()
                .HasMany(c => c.Movies)
                .WithMany(m => m.Characters)
                .UsingEntity(j =>
                {
                    j.ToTable("CharacterMovie"); // Define the name of the join table
                    j.Property<int>("CharacterId"); // Define shadow property for CharacterId
                    j.Property<int>("MovieId"); // Define shadow property for MovieId
                    j.HasKey("CharacterId", "MovieId"); // Define the composite primary key
                    j.HasData(
                        new { CharacterId = 1, MovieId = 1 },
                        new { CharacterId = 2, MovieId = 1 },
                        new { CharacterId = 2, MovieId = 2 },
                        new { CharacterId = 3, MovieId = 3 }
                    );
                });


        }


    }

}
