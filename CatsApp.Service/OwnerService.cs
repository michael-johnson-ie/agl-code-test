using System.Collections.Generic;
using CatsApp.Dto;
using System.Linq;
using CatsApp.Repository;
using CatsApp.Model;

namespace CatsApp.Service
{
    public class OwnerService : IOwnerService
    {
        private IOwnerRepository _ownerRepository;

        public OwnerService(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        public IEnumerable<GenderDto> GetGendersWithCats()
        {
            var owners = _ownerRepository.GetOwners();
            var petType = PetType.Cat;

            return owners.
                GroupBy(o => o.Gender, o => o.Pets,
                    (g, p) => new GenderDto
                    {
                        Title = g.ToString(),
                        Pets = BuildPetsDto(
                            p.Where(x=> x != null).
                            SelectMany(x => x).
                            Distinct(),
                            petType)
                    }
                );
        }

        private IEnumerable<PetDto> BuildPetsDto(IEnumerable<Pet> pets, PetType? petType = null)
        {
            return pets.
                Where(p => petType == null || p.Type == petType.Value).
                OrderBy(p => p.Name).
                Select(p => new PetDto()
                {
                    Name = p.Name
                });
        }
    }
}