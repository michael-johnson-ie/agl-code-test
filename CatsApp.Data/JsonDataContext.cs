using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CatsApp.Data
{
    public class JsonDataContext : IDataContext
    {
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
            var client = new HttpClient();
            return await client.GetAsync("http://agl-developer-test.azurewebsites.net/people.json");
        }

    }
}
