using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CatsApp.Data
{
    public class JsonDataContext : IDataContext
    {
        public readonly HttpClient _httpClient;

        public JsonDataContext(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IEnumerable<T> Get<T>()
        {
            return GetData<T>();
        }

        private IEnumerable<T> GetData<T>()
        {
            IEnumerable<T> data = null;
            var task = GetClient()
              .ContinueWith((taskwithresponse) =>
              {
                  var response = taskwithresponse.Result;
                  var jsonString = response.Content.ReadAsStringAsync();
                  jsonString.Wait();
                  data = JsonConvert.DeserializeObject<List<T>>(jsonString.Result);

              });
            task.Wait();

            return data;
        }

        private async Task<HttpResponseMessage> GetClient()
        {
            return await _httpClient.GetAsync("http://agl-developer-test.azurewebsites.net/people.json");
        }
    }
}
