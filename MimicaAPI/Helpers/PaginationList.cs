using MimicaAPI.V1.Models.DTO;
using System.Collections.Generic;

namespace MimicaAPI.Helpers
{
    public class PaginationList<T>
    {
        public List<T> Results { get; set; } = new List<T>();

        public Paginacao Paginacao { get; set; }

        public List<LinkDTO> Links { get; set; } = new List<LinkDTO>();
    }
}
