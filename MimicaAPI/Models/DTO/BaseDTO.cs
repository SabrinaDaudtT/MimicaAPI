using System.Collections.Generic;

namespace MinicAPI.Models.DTO
{
    public abstract class BaseDTO
    {
        public List<LinkDTO> Links { get; set; }
    }
}
