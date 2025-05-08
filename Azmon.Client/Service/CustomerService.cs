using Azmon.Core;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace Azmon.Client.Service
{
    
    public class CustomerService
    {
        private readonly HttpClient _http;
        public CustomerService(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<Customer>?> GetAllAsync() =>
       await _http.GetFromJsonAsync<List<Customer>>("api/Customers");

        public async Task<List<Customer>?> SearchAsync(string query) =>
            await _http.GetFromJsonAsync<List<Customer>>($"api/Customers/search/{query}");

        public async Task<Customer?> GetByIdAsync(int id) =>
         await _http.GetFromJsonAsync<Customer>($"api/Customers/{id}");

        public async Task CreateAsync(Customer customer) =>
            await _http.PostAsJsonAsync("api/Customers", customer);

        public async Task UpdateAsync(int id, Customer customer) =>
            await _http.PutAsJsonAsync($"api/Customers/{id}", customer);
        public async Task DeleteAsync(int id) =>
            await _http.DeleteAsync($"api/Customers/{id}");

    }

}
