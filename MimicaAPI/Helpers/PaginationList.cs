using MimicaAPI.Models.DTO;
using System.Collections.Generic;

namespace MimicaAPI.Helpers
{
    public class PaginationList<T>
    {
        public List<T> Results { get; set; }

        public Paginacao Paginacao { get; set; }

        public List<LinkDTO> Links { get; set; }
    }
}
