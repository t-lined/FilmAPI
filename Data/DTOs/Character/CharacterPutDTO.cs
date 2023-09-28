namespace FilmAPI.Data.DTOs.Character
{
    public class CharacterPutDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string? Alias { get; set; }
        public string Gender { get; set; }

        public string PictureUrl { get; set; }
    }
}
