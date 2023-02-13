
using System.Collections.Generic;

namespace MimicaAPI.V1.Models.DTO
{
    public abstract class BaseDTO
    {
        public List<LinkDTO> Links { get; set; } = new List<LinkDTO>();
    }
}
