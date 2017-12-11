using System;
using System.Collections.Generic;
using System.Linq;
using CatsApp.Dto;
using CatsApp.Service;

namespace CatsApp.ConsoleApp
{
    public class Application
    {
        private readonly IOwnerService _ownerService;

        public Application(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        private void OutputGenders(IList<GenderDto> genders, int i = 0)
        {
            var gender = genders[i];

            Console.WriteLine(gender.Title);

            if (gender.Pets?.Any() == true)
            {
                OutputPets(gender.Pets.ToList());
            }

            i++;

            if (genders.Count > i) OutputGenders(genders, i);
        }

        private void OutputPets(IList<PetDto> pets, int i = 0)
        {
            Console.WriteLine($" - {pets[i].Name}");
            i++;

            if (pets.Count > i) OutputPets(pets, i);
        }

        public void Run()
        {
            var genders = _ownerService.GetGendersForPetType(Model.PetType.Cat).ToList();

            if (genders.Any())
            {
                OutputGenders(genders);
            }
        }
    }
}