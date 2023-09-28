using AutoMapper;
using FilmAPI.Data.DTOs.Movie;
using FilmAPI.Models;

namespace FilmAPI.Mappers
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MoviePostDTO>().ReverseMap();

            CreateMap<Movie, MovieDTO>()
                .ForMember(mdto => mdto.Characters, options => options
                    .MapFrom(m => m.Characters.Select(c => c.Id).ToArray()));

            CreateMap<Movie, MoviePutDTO>().ReverseMap();
        }
    }
}
