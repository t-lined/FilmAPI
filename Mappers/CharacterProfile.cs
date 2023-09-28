using AutoMapper;
using FilmAPI.Data.DTOs.Character;
using FilmAPI.Models;

namespace FilmAPI.Mappers
{

    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<Character, CharacterPostDTO>().ReverseMap();

            CreateMap<Character, CharacterDTO>()
                .ForMember(cdto => cdto.Movies, options => options
                    .MapFrom(c => c.Movies.Select(m => m.Id).ToArray()));

            CreateMap<Character, CharacterPutDTO>().ReverseMap();
        }



    }

}
