using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CatProvider;
using CatProvider.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System.Linq;

namespace CatDataServiceTests
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod()]
        public async Task GetImageTest()
        {
            //arrange
            var expectedImageList = new List<Image>
    {
        new Image() { Id="23v", Url="https://cdn2.thecatapi.com/images/23v.jpg"},
        new Image() { Id="3n1", Url="https://cdn2.thecatapi.com/images/3n1.gif"},
        new Image() { Id="NwMUoJYmT", Url="https://cdn2.thecatapi.com/images/NwMUoJYmT.jpg"},
        new Image() { Id="FGtYc9CUT", Url="https://cdn2.thecatapi.com/images/FGtYc9CUT.jpg"},
    };
            var json = JsonConvert.SerializeObject(expectedImageList);

            HttpResponseMessage httpResponse = new HttpResponseMessage();
            httpResponse.StatusCode = System.Net.HttpStatusCode.OK;
            httpResponse.Content = new StringContent(json);

            var mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            mockHttpClientWrapper.Setup(t => t.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(httpResponse);

            CatDataService service = new CatDataService(mockHttpClientWrapper.Object);

            //act
            var actualImageList = await service.GetImage("23v");

            //assert
            CollectionAssert.AreNotEquivalent(expectedImageList, actualImageList);
        }
    }
}
