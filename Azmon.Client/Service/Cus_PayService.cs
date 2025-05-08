using Azmon.Core;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace Azmon.Client.Service
{
    public class Cus_PayService
    {
        private readonly HttpClient _http;
        public Cus_PayService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Cus_Pay>?> GetAllAsync() =>
       await _http.GetFromJsonAsync<List<Cus_Pay>>("api/Cus_Pay");

        public async Task<Cus_Pay?> GetByIdAsync(int id) =>
            await _http.GetFromJsonAsync<Cus_Pay>($"api/Cus_Pay/{id}");

        public async Task<List<Cus_Pay>?> GetByCIdAsync(int customerId) =>
            await _http.GetFromJsonAsync<List<Cus_Pay>>($"api/Cus_Pay/Customer/{customerId}");

        public async Task CreateAsync(Cus_Pay pay) =>
            await _http.PostAsJsonAsync("api/Cus_Pay", pay);

        public async Task UpdateAsync(int id, Cus_Pay pay)
        {
            var res = await _http.PutAsJsonAsync($"api/Cus_Pay/{id}", pay);
            res.EnsureSuccessStatusCode(); // يرمي خطأ إذا فشل
        }

        public async Task DeleteAsync(int id) =>
            await _http.DeleteAsync($"api/Cus_Pay/{id}");

    }
}