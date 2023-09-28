using AutoMapper;
using FilmAPI.Data.DTOs.Franchise;
using FilmAPI.Models;

namespace FilmAPI.Mappers
{
    public class FranchiseProfile : Profile
    {
        public FranchiseProfile()
        {
            CreateMap<Franchise, FranchisePostDTO>().ReverseMap();

            CreateMap<Franchise, FranchiseDTO>()
                .ForMember(cdto => cdto.Movies, options => options
                    .MapFrom(c => c.Movies.Select(m => m.Id).ToArray()));

            CreateMap<Franchise, FranchisePutDTO>().ReverseMap();
        }
    }
}
