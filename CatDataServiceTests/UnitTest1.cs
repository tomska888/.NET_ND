using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CatProvider;
using CatProvider.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;

namespace CatDataServiceTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod()]
        public async Task GetBreedSearchTest()
        {
            //arrange
            var expectedBreedList = new List<Breed>
            {
                new Breed() { Id="abys",
            Description="The Abyssinian is easy to care for, and a joy to have in your home. " +
            "They’re affectionate cats and love both people and other animals.",
            LifeSpan="14 - 15",
            Name="Abyssinian",
            Origin="Egypt",
            Temperament="Active, Energetic, Independent, Intelligent, Gentle",
            WikipediaUrl="https://en.wikipedia.org/wiki/Abyssinian_(cat)"},
        new Breed() { Id="aege",
            Description="Native to the Greek islands known as the Cyclades in the Aegean Sea, " +
            "these are natural cats, meaning they developed without humans getting involved in their breeding. " +
            "As a breed, Aegean Cats are rare, although they are numerous on their home islands. " +
            "They are generally friendly toward people and can be excellent cats for families with children.",
            LifeSpan="9 - 12",
            Name="Aegean",
            Origin="Greece",
            Temperament="Affectionate, Social, Intelligent, Playful, Active",
            WikipediaUrl="https://en.wikipedia.org/wiki/Aegean_cat"},
        new Breed() { Id="abob",
            Description="American Bobtails are loving and incredibly intelligent cats possessing a distinctive wild appearance. " +
            "They are extremely interactive cats that bond with their human family with great devotion.",
            LifeSpan="11 - 15",
            Name="American Bobtail",
            Origin="United States",
            Temperament="Intelligent, Interactive, Lively, Playful, Sensitive",
            WikipediaUrl="https://en.wikipedia.org/wiki/American_Bobtail"},
        new Breed() { Id="amau",
            Description="rabian Mau cats are social and energetic. Due to their energy levels, " +
            "these cats do best in homes where their owners will be able to provide them with plenty " +
            "of playtime, attention and interaction from their owners. These kitties are friendly, " +
            "intelligent, and adaptable, and will even get along well with other pets and children.",
            LifeSpan="12 - 14",
            Name="American Curl",
            Origin="United Arab Emirates",
            Temperament="Affectionate, Agile, Curious, Independent, Playful, Loyal",
            WikipediaUrl="https://en.wikipedia.org/wiki/Arabian_Mau"},
            };

            var json = JsonConvert.SerializeObject(expectedBreedList);

            HttpResponseMessage httpResponse = new HttpResponseMessage();
            httpResponse.StatusCode = System.Net.HttpStatusCode.OK;
            httpResponse.Content = new StringContent(json);

            var mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            mockHttpClientWrapper.Setup(t => t.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(httpResponse);

            CatDataService service = new CatDataService(mockHttpClientWrapper.Object);

            //act
            var actualBreedList = await service.GetBreedSearch("beng");

            //assert
            CollectionAssert.AreEquivalent(expectedBreedList, actualBreedList);
        }
    }
}
