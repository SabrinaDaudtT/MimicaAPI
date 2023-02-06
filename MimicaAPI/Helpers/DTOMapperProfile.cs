using AutoMapper;
using MimicaAPI.Helpers;
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
            CreateMap<PaginationList<Palavra>, PaginationList<PalavraDTO>>();
        }
    }
}
