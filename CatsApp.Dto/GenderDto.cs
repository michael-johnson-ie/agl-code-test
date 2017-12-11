using System.Collections.Generic;

namespace CatsApp.Dto
{
    public class GenderDto
    {
        public string Title { get; set; }

        public IEnumerable<PetDto> Pets { get; set; }
    }
}