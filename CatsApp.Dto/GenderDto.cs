using System.Collections.Generic;
using CatsApp.Common;

namespace CatsApp.Dto
{
    public class GenderDto
    {
        public Gender Gender { get; set; }

        public IEnumerable<PetDto> Pets { get; set; }
    }
}