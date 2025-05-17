using Microsoft.JSInterop;
using ServiceStack;

namespace Azmon.Client.Service
{
   
    public class ReportService
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _js;
        public ReportService(HttpClient http, IJSRuntime js)
        {
            _http = http;
            _js = js;
        }
        public async Task SellReportPdf(int customerId)
        {
            var fileName = "SellReport.pdf";

            var response = await _http.GetAsync($"api/SReports/SellReportPdf/{customerId}");
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                using var streamRef = new DotNetStreamReference(stream);

                await _js.InvokeVoidAsync("downloadFileFromStream", fileName, streamRef);
            }
        }

        public async Task OneSellReportPdf(int SellId)
        {
            var fileName = "SellReport.pdf";

            var response = await _http.GetAsync($"api/SReports/OneSellReportPdf/{SellId}");
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                using var streamRef = new DotNetStreamReference(stream);

                await _js.InvokeVoidAsync("downloadFileFromStream", fileName, streamRef);
            }
        }

        public async Task PaymentReportPdf(int customerId)
        {
            var fileName = "PaymentReport.pdf";

            var response = await _http.GetAsync($"api/SReports/PaymentReportPdf/{customerId}");
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                using var streamRef = new DotNetStreamReference(stream);

                await _js.InvokeVoidAsync("downloadFileFromStream", fileName, streamRef);
            }
        }

        public async Task OnePaymentReportPdf(int payId)
        {
            var fileName = "PaymentReport.pdf";

            var response = await _http.GetAsync($"api/SReports/OnePaymentReportPdf/{payId}");
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                using var streamRef = new DotNetStreamReference(stream);

                await _js.InvokeVoidAsync("downloadFileFromStream", fileName, streamRef);
            }
        }

    }
}
