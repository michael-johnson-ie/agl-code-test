using System.Linq;
using System.Net.Http;
using CatsApp.Data;
using CatsApp.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using RichardSzalay.MockHttp;

namespace CatsApp.UnitTests
{
    [TestClass]
    public class JsonDataContextTests
    {
        private HttpClient _httpClient;
        private JsonDataContext _jsonDataContext;
        private MockHttpMessageHandler _mockHttp;

        public void Setup()
        {
            _mockHttp = new MockHttpMessageHandler();

            _mockHttp.When("*")
           .Respond("application/json", @"[{ ""name"":""Bob"",""gender"":""Male"",""age"":23,""pets"":[{""name"":""Garfield"",""type"":""Cat""}]}]");

            _httpClient = new HttpClient(_mockHttp);
            _jsonDataContext = new JsonDataContext(_httpClient);
        }

        [TestMethod]
        public void JsonDataContextGet_ReturnsJsonifiedOwner()
        {
            Setup();

            var owners = _jsonDataContext.Get<Owner>().ToList();

            Assert.AreEqual(1, owners.Count());
            Assert.AreEqual("Bob", owners.First().Name);
            Assert.AreEqual(1, owners.First().Pets.Count());
            Assert.AreEqual("Garfield", owners.First().Pets.First().Name);
        }
    }
}
