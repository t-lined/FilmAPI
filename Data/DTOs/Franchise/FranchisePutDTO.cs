using System.ComponentModel.DataAnnotations;

namespace FilmAPI.Data.DTOs.Franchise
{
    public class FranchisePutDTO
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Description { get; set; }

    }
}
