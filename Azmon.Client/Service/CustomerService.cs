using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace Azmon.Client.Service
{

    public class CustomerService
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _js;

        public CustomerService(HttpClient http, IJSRuntime js)
        {
            _http = http;
            _js = js;
        }
        public async Task<List<Core.Customer>?> GetAllAsync() =>
       await _http.GetFromJsonAsync<List<Core.Customer>>("api/Customers");

        public async Task<List<Core.Customer>?> SearchAsync(string query) =>
            await _http.GetFromJsonAsync<List<Core.Customer>>($"api/Customers/search/{query}");

        public async Task<Core.Customer?> GetByIdAsync(int id) =>
         await _http.GetFromJsonAsync<Core.Customer>($"api/Customers/{id}");

        public async Task CreateAsync(Core.Customer customer) =>
            await _http.PostAsJsonAsync("api/Customers", customer);

        public async Task UpdateAsync(int id, Core.Customer customer) =>
            await _http.PutAsJsonAsync($"api/Customers/{id}", customer);
        public async Task DeleteAsync(int id) =>
            await _http.DeleteAsync($"api/Customers/{id}");


        /* public async Task UpdateBalanceAsync(int id, Core.Customer customer) =>
               await _http.PutAsJsonAsync($"api/Customers/calculate-balance/{id}", customer);
 */
        public async Task CalculateAndSaveBalance(int id, Core.Customer customer) =>
         await _http.PutAsJsonAsync($"api/Customers/calculate-balance/{id}", customer);

    }

}
