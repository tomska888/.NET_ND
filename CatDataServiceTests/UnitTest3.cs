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
    public class UnitTest3
    {
        [TestMethod()]
        public async Task GetCategoryTest()
        {
            //arrange
            var expectedCategoryList = new List<Category>
    {
        new Category() { Id="1", Name="hats"},
        new Category() { Id="2", Name="space"},
        new Category() { Id="3", Name="funny"},
        new Category() { Id="4", Name="sunglasses"},
    };
            var json = JsonConvert.SerializeObject(expectedCategoryList);

            string url = "https://api.thecatapi.com";

            HttpResponseMessage httpResponse = new HttpResponseMessage();
            httpResponse.StatusCode = System.Net.HttpStatusCode.OK;
            httpResponse.Content = new StringContent(json);

            var mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            mockHttpClientWrapper.Setup(t => t.GetAsync(It.Is<string>(s => s.StartsWith(url))))
                .ReturnsAsync(httpResponse);

            CatDataService service = new CatDataService(mockHttpClientWrapper.Object);

            //act
            var actualCategoryList = await service.GetCategory(1, 2);

            //assert
            CollectionAssert.AreEquivalent(expectedCategoryList, actualCategoryList);
        }
    }
}
