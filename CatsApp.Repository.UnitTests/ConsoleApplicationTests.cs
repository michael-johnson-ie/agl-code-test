using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using CatsApp.Model;
using CatsApp.Service;
using CatsApp.Dto;
using CatsApp.ConsoleApp;
using System.IO;

namespace CatsApp.UnitTests
{
    [TestClass]
    public class ConsoleApplicationTests
    {
        private IOwnerService _ownerService;
        private Application _application;

        public void Setup()
        {
            _ownerService = Substitute.For<IOwnerService>();
            _application = new Application(_ownerService);
        }

        [TestMethod]
        public void ApplicationRun_OutputsCorrectString_ForGenders()
        {
            Setup();

            var genders = new List<GenderDto>
            {
                new GenderDto { Title = "Male", Pets = new List<PetDto> { new PetDto { Name = "AAA" }, new PetDto { Name = "BBB" } } },
                new GenderDto { Title = "Female", Pets = new List<PetDto> { new PetDto { Name = "CCC" }, new PetDto { Name = "DDD" } } }
            };

            _ownerService.GetGendersForPetType(PetType.Cat).Returns(genders);

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                _application.Run();

                var expected = string.Format("Male{0} - AAA{0} - BBB{0}Female{0} - CCC{0} - DDD{0}", Environment.NewLine);
                Assert.AreEqual(expected, sw.ToString());
            }

            _ownerService.Received(1).GetGendersForPetType(PetType.Cat);
        }

        [TestMethod]
        public void ApplicationRun_CreatesZeroOutput_ForEmptyGenders()
        {
            Setup();

            var genders = new List<GenderDto>();

            _ownerService.GetGendersForPetType(PetType.Cat).Returns(genders);

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                _application.Run();

                var expected = string.Empty;
                Assert.AreEqual(expected, sw.ToString());
            }

            _ownerService.Received(1).GetGendersForPetType(PetType.Cat);
        }

        [TestMethod]
        public void ApplicationRun_CreatesCorrectOutput_ForGendersWithNoPets()
        {
            Setup();

            var genders = new List<GenderDto>
            {
                new GenderDto { Title = "Male" },
                new GenderDto { Title = "Female", Pets = new List<PetDto> { new PetDto { Name = "CCC" } } }
            };

            _ownerService.GetGendersForPetType(PetType.Cat).Returns(genders);

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                _application.Run();

                var expected = string.Format("Male{0}Female{0} - CCC{0}", Environment.NewLine);
                Assert.AreEqual(expected, sw.ToString());
            }

            _ownerService.Received(1).GetGendersForPetType(PetType.Cat);
        }
    }
}
