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
    public class UnitTest4
    {
        [TestMethod()]
        public async Task GetFavouritesTest()
        {
            //arrange
            var expectedFavouritesList = new List<Favourite>
    {
        new Favourite() { ImageId="b9c"},
        new Favourite() { ImageId="MjA3NTE1MA"},
        new Favourite() { ImageId="6cs"},
        new Favourite() { ImageId="MjA3MjI3Mw"},
    };
            var json = JsonConvert.SerializeObject(expectedFavouritesList);

            HttpResponseMessage httpResponse = new HttpResponseMessage();
            httpResponse.StatusCode = System.Net.HttpStatusCode.OK;
            httpResponse.Content = new StringContent(json);

            var mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            mockHttpClientWrapper.Setup(t => t.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(httpResponse);

            CatDataService service = new CatDataService(mockHttpClientWrapper.Object);

            //act
            var actualFavouritesList = await service.GetFavourites();

            //assert
            CollectionAssert.Contains(expectedFavouritesList, actualFavouritesList);
        }
    }
}
