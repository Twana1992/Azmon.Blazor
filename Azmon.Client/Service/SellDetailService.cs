using Azmon.Core;
using System.Net.Http.Json;

namespace Azmon.Client.Service
{
    public class SellDetailService
    {
        private readonly HttpClient _http;

        public SellDetailService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Sell_Detail>?> GetAllAsync() =>
        await _http.GetFromJsonAsync<List<Sell_Detail>>("api/Sell_Detail");


        public async Task<Sell_Detail?> GetByIdAsync(int id) =>
        await _http.GetFromJsonAsync<Sell_Detail>($"api/Sell_Detail/{id}");

        public async Task<List<Sell_Detail>> GetBySellIdAsync(int sellId)
        {
            var result = await _http.GetFromJsonAsync<List<Sell_Detail>>($"api/Sell_Detail/SellId/{sellId}");
            return result ?? new List<Sell_Detail>();
        }



        public async Task CreateAsync(Sell_Detail detail) =>
           await _http.PostAsJsonAsync("api/Sell_Detail", detail);


        public async Task UpdateAsync(int id, Sell_Detail detail) =>

        await _http.PutAsJsonAsync($"api/Sell_Detail/{id}", detail);


        public async Task DeleteAsync(int id) =>
         await _http.DeleteAsync($"api/Sell_Detail/{id}");

    }
}
