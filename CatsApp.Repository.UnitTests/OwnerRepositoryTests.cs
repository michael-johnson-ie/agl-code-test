using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using CatsApp.Data;
using CatsApp.Model;
using CatsApp.Repository;
using System.Linq;

namespace CatsApp.UnitTests
{
    [TestClass]
    public class OwnerRepositoryTests
    {
        private IDataContext _dataContext;
        private OwnerRepository ownerRepository;
        private IEnumerable<Owner> _owners;

        public void Setup()
        {
            _dataContext = Substitute.For<IDataContext>();
            ownerRepository = new OwnerRepository(_dataContext);
        }

        [TestMethod]
        public void GetOwners_ReturnsOwners()
        {
            Setup();

            _owners = new List<Owner>
            {
                new Owner { Name = "AAA", Age = 30, Gender = Gender.Female, Pets = new List<Pet> { new Pet { Name = "AAA_Pet", Type = PetType.Cat } } },
                new Owner { Name = "BBB", Age = 40, Gender = Gender.Male, Pets = new List<Pet> { new Pet { Name = "BBB_Pet", Type = PetType.Fish } } },
                new Owner { Name = "CCC", Age = 50, Gender = Gender.Female, Pets = new List<Pet> { new Pet { Name = "CCC_Pet", Type = PetType.Dog } } }
            };

            _dataContext.Get<Owner>().Returns(_owners);

            var owners = ownerRepository.GetOwners();

            Assert.AreEqual(3, owners.Count());
            Assert.AreEqual(owners, _owners);
            _dataContext.Received(1).Get<Owner>();
        }

        [TestMethod]
        public void GetOwners_WithNoResults_ReturnsEmptyOwners()
        {
            Setup();

            var owners = ownerRepository.GetOwners();

            Assert.AreEqual(0, owners.Count());
            _dataContext.Received(1).Get<Owner>();
        }
    }
}
