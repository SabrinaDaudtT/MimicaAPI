using AutoMapper;
using MimicaAPI.Helpers;
using MimicaAPI.V1.Models;
using MimicaAPI.V1.Models.DTO;

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
