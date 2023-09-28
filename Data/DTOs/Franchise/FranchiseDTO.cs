﻿namespace FilmAPI.Data.DTOs.Franchise
{
    public class FranchiseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int[] Movies { get; set; } // Navigation property
    }
}
