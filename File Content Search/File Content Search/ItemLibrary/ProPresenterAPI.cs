using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace File_Content_Search.ItemLibrary
{
    public class ProPresenterAPI
    {
        private static readonly HttpClient client = new HttpClient();
        private string baseUrl;

        public ProPresenterAPI(string port)
        {
            baseUrl = $"http://localhost:{port}/v1";
        }

        public async Task<JArray> GetLibrariesAsync()
        {
            var response = await client.GetAsync($"{baseUrl}/libraries");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            return JArray.Parse(responseBody);
        }

        public async Task<JObject> GetLibraryAsync(string libraryId)
        {
            var response = await client.GetAsync($"{baseUrl}/library/{libraryId}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return JObject.Parse(responseBody);
        }

        public async Task<JObject> GetPresentationAsync(string presentationId)
        {
            var response = await client.GetAsync($"{baseUrl}/presentation/{presentationId}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return JObject.Parse(responseBody);
        }
    }
}
