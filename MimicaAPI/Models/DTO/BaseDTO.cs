
using System.Collections.Generic;

namespace MimicaAPI.Models.DTO
{
    public abstract class BaseDTO
    {
        public List<LinkDTO> Links { get; set; } = new List<LinkDTO>();
    }
}
