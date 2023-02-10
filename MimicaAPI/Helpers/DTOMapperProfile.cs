using AutoMapper;
using MimicaAPI.Models;
using MinicAPI.Models;
using MinicAPI.Models.DTO;

namespace MinicAPI.Helpers
{
    public class DTOMapperProfile : Profile
    {
        public DTOMapperProfile()
        {
            CreateMap<Palavra, PalavraDTO>();
        }
    }
}
