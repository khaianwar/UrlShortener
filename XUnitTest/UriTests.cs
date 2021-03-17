using Newtonsoft.Json;
using Repository.Model;
using Repository.Service;
using UrlShortener.Controllers;
using UrlShortener.Model;
using Xunit;

namespace XUnitTest
{
    public class UriTests
    {
        private UriService CreateMockService()
        {
            var uriService = new UriService(new UriDatabaseSettings()
            {
                DatabaseName = "UrlShortener",
                ConnectionString = "mongodb+srv://user:1234@urlshortener.ecclo.mongodb.net/myFirstDatabase?retryWrites=true&w=majority",
                UriCollection = "Uris"
            });
            return uriService;
        }

        [Fact]
        public void GetUri_Invalid()
        {
            var uriService = CreateMockService();
            var uriController = new UriController(uriService);
            var jsonResult = uriController.Get("invalidUrl");
            var result = JsonConvert.DeserializeObject<UriResultModel>(jsonResult);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void GetUri_New()
        {
            var uriService = CreateMockService();
            var uriController = new UriController(uriService);
            var jsonResult = uriController.Get("http://www.google.com");
            var result = JsonConvert.DeserializeObject<UriResultModel>(jsonResult);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void GetUri_Existing()
        {
            var uriService = CreateMockService();
            var uriController = new UriController(uriService);
            var jsonResult1 = uriController.Get("http://www.google.com");
            var result1 = JsonConvert.DeserializeObject<UriResultModel>(jsonResult1);
            var jsonResult2 = uriController.Get("http://www.google.com");
            var result2 = JsonConvert.DeserializeObject<UriResultModel>(jsonResult2);
            Assert.Equal(result1.Url, result2.Url);
        }
    }
}
