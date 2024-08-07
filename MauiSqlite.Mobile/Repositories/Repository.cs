using MauiSqlite.Mobile.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiSqlite.Mobile.Repositories
{
    public class Repository : IRepository
    {
        private JsonSerializerOptions _jsonDefaultOptions => new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        private async Task<T> UnserializeAnswer<T>(HttpResponseMessage httpResponse, JsonSerializerOptions jsonSerializerOptions)
        {
            var respuestaString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(respuestaString, jsonSerializerOptions)!;
        }

        public async Task<HttpResponseWrapper<T>> GetPersoN<T>(string urlBase, string url, LoginDTO modeld)
        {
            var messageJSON = JsonSerializer.Serialize(modeld);
            var messageContet = new StringContent(messageJSON, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(urlBase)
            };
            var responseHttp = await client.PostAsync(url, messageContet);
            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await UnserializeAnswer<T>(responseHttp, _jsonDefaultOptions);
                return new HttpResponseWrapper<T>(response, false, responseHttp);
            }
            return new HttpResponseWrapper<T>(default, true, responseHttp);
        }

        public async Task<HttpResponseWrapper<T>> Get<T>(string urlBase, string url)
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(urlBase)
            };
            var responseHttp = await client.GetAsync(url);

            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await UnserializeAnswer<T>(responseHttp, _jsonDefaultOptions);
                return new HttpResponseWrapper<T>(response, false, responseHttp);
            }

            return new HttpResponseWrapper<T>(default, true, responseHttp);
        }
    }
}
