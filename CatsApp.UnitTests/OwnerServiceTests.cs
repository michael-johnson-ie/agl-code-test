using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using CatsApp.Model;
using CatsApp.Repository;
using System.Linq;
using CatsApp.Service;
using CatsApp.Common;

namespace CatsApp.UnitTests
{
    [TestClass]
    public class OwnerServiceTests
    {
        private IOwnerRepository _ownerRepository;
        private OwnerService _ownerService;

        public void Setup()
        {
            _ownerRepository = Substitute.For<IOwnerRepository>();
            _ownerService = new OwnerService(_ownerRepository);
        }

        [TestMethod]
        public void GetGendersWithPetType_WithAllGendersHavingPetType_ReturnsAllGenders()
        {
            Setup();

            var _owners = new List<Owner>
            {
                new Owner { Name = "AAA", Age = 30, Gender = Gender.Female, Pets = new List<Pet> { new Pet { Name = "AAA_Pet", Type = PetType.Cat } } },
                new Owner { Name = "BBB", Age = 40, Gender = Gender.Male, Pets = new List<Pet> { new Pet { Name = "BBB_Pet", Type = PetType.Cat } } },
                new Owner { Name = "CCC", Age = 50, Gender = Gender.Female, Pets = new List<Pet> { new Pet { Name = "CCC_Pet", Type = PetType.Dog }, new Pet { Name = "CCC_Pet2", Type = PetType.Cat } } }
            };

            _ownerRepository.GetOwners().Returns(_owners);

            var genders = _ownerService.GetGendersForPetType(PetType.Cat).ToList();

            Assert.AreEqual(2, genders.Count());
            Assert.AreEqual(Gender.Female, genders[0].Gender);
            Assert.AreEqual(2, genders[0].Pets.Count());
            Assert.AreEqual(Gender.Male, genders[1].Gender);
            Assert.AreEqual(1, genders[1].Pets.Count());
            _ownerRepository.Received(1).GetOwners();
        }

        [TestMethod]
        public void GetGendersWithPetType_WithNoGendersHavingPetType_ReturnsNoGenders()
        {
            Setup();

            var _owners = new List<Owner>
            {
                new Owner { Name = "AAA", Age = 30, Gender = Gender.Female, Pets = new List<Pet> { new Pet { Name = "AAA_Pet", Type = PetType.Dog } } },
                new Owner { Name = "BBB", Age = 40, Gender = Gender.Male, Pets = new List<Pet> { new Pet { Name = "BBB_Pet", Type = PetType.Fish } } },
                new Owner { Name = "CCC", Age = 50, Gender = Gender.Female, Pets = new List<Pet> { new Pet { Name = "CCC_Pet", Type = PetType.Dog } } }
            };

            _ownerRepository.GetOwners().Returns(_owners);

            var genders = _ownerService.GetGendersForPetType(PetType.Cat);

            Assert.AreEqual(0, genders.Count());
            _ownerRepository.Received(1).GetOwners();
        }

        [TestMethod]
        public void GetGendersWithPetType_WithGenderHavingPetsOfType_ReturnsAllPets()
        {
            Setup();

            var _owners = new List<Owner>
            {
                new Owner { Name = "AAA", Age = 30, Gender = Gender.Male, Pets = new List<Pet> { new Pet { Name = "AAA_Pet", Type = PetType.Cat } } },
                new Owner { Name = "BBB", Age = 40, Gender = Gender.Male, Pets = new List<Pet> { new Pet { Name = "BBB_Pet", Type = PetType.Cat } } },
                new Owner { Name = "CCC", Age = 50, Gender = Gender.Male, Pets = new List<Pet> { new Pet { Name = "CCC_Pet", Type = PetType.Cat }, new Pet { Name = "CCC_Pet2", Type = PetType.Cat } } }
            };

            _ownerRepository.GetOwners().Returns(_owners);

            var genders = _ownerService.GetGendersForPetType(PetType.Cat);

            Assert.AreEqual(1, genders.Count());
            Assert.AreEqual(Gender.Male, genders.First().Gender);
            Assert.AreEqual(4, genders.ToList()[0].Pets.Count());
            _ownerRepository.Received(1).GetOwners();
        }

        [TestMethod]
        public void GetGendersWithPetType_WithZeroPets_DoesNotReturnGender()
        {
            Setup();

            var _owners = new List<Owner>
            {
                new Owner { Name = "AAA", Age = 30, Gender = Gender.Female },
                new Owner { Name = "BBB", Age = 40, Gender = Gender.Male, Pets = new List<Pet> { new Pet { Name = "BBB_Pet", Type = PetType.Cat } } },
                new Owner { Name = "CCC", Age = 50, Gender = Gender.Male, Pets = new List<Pet> { new Pet { Name = "CCC_Pet", Type = PetType.Cat }, new Pet { Name = "CCC_Pet2", Type = PetType.Cat } } }
            };

            _ownerRepository.GetOwners().Returns(_owners);

            var genders = _ownerService.GetGendersForPetType(PetType.Cat);

            Assert.AreEqual(1, genders.Count());
            Assert.AreEqual(Gender.Male, genders.ToList().First().Gender);
            _ownerRepository.Received(1).GetOwners();
        }
    }
}
