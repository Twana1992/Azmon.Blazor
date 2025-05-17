using System.Net.Http.Json;

namespace Azmon.Client.Service
{
    public class CustomerBalanceService
    {
       
        private readonly HttpClient _http;
        public CustomerBalanceService(HttpClient http)
        {
            _http = http;
        }

        public async Task CalculateAndSaveBalance(int id, Core.Customer customer) =>
         await _http.PutAsJsonAsync($"api/Customers/calculate-balance/{id}", customer);

       /* public async Task CalculateAndSaveBalance(int id, Core.Customer customer) =>
         await _http.PutAsJsonAsync($"Customer/calculate-balance/{id}", customer);*/
    }

}

