using Azmon.Core;
using System.Net.Http.Json;

namespace Azmon.Client.Service
{
    public class SellService
    {
        private readonly HttpClient _http;

        public SellService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Sell>?> GetAllAsync()=>
         await _http.GetFromJsonAsync<List<Sell>>("api/Sells");
        

        public async Task<Sell?> GetByIdAsync(int id)=>
        await _http.GetFromJsonAsync<Sell>($"api/Sells/{id}");


        public async Task<List<Sell>?> GetByCIdAsync(int customerId) =>
          await _http.GetFromJsonAsync<List<Sell>>($"api/Sells/Customer/{customerId}");


        public async Task<Sell?> CreateAsync(Sell sell)
        {
            var response = await _http.PostAsJsonAsync("api/Sells", sell);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"🚨 HTTP Error {response.StatusCode}: {errorContent}");
                return null;
            }

            return await response.Content.ReadFromJsonAsync<Sell>();
        }



        public async Task UpdateAsync(int id, Sell sell)=>
         await _http.PutAsJsonAsync($"api/Sells/{id}", sell);
           
        

        public async Task DeleteAsync(int id)=>
        await _http.DeleteAsync($"api/Sells/{id}");
        
    }
}
