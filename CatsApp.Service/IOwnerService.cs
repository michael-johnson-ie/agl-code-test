using System.Collections.Generic;
using CatsApp.Dto;

namespace CatsApp.Service
{
    public interface IOwnerService
    {
        IEnumerable<GenderDto> GetGendersWithCats();
    }
}
