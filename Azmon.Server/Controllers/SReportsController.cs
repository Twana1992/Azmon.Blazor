using Azmon.Server.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Azmon.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SReportsController : ControllerBase
    {
        private readonly DBContext _context;
       
        public SReportsController(DBContext dBContext)
        {
            _context=dBContext; 
        }
        [HttpGet("SellReportPdf/{customerId}")]
        public IActionResult GenerateSellReport(int customerId)
        {
            var customer = _context.Customer.FirstOrDefault(c => c.Id == customerId);
            if (customer == null)
                return NotFound("Customer Not Found");

            var sells = _context.Sell
                .Where(s => s.CustomerId == customerId)
                .ToList();

            var sellIds = sells.Select(s => s.Id).ToList();

            var details = _context.Sell_Detail
                .Where(sd => sellIds.Contains(sd.SellId))
                .ToList();

            var productIds = details.Select(d => d.ProductId).Distinct().ToList();

            var products = _context.Product
                .Where(p => productIds.Contains(p.Id))
                .ToList();

            var stream = new MemoryStream();

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Content().Column(col =>
                    {
                        col.Item().PaddingBottom(10).Text($"Sell Report for: {customer.Name}")
                            .FontSize(20).Bold().AlignCenter();

                        foreach (var sell in sells)
                        {
                            var sellDetails = details.Where(d => d.SellId == sell.Id).ToList();

                            col.Item().PaddingVertical(10).Text($"Invoes Id: {sell.Id} | Date: {sell.BillDate:yyyy/MM/dd} | Total: {sell.Price}")
                                .FontSize(14).Bold().FontColor(Colors.Blue.Darken1);

                            col.Item().Table(table =>
                            {
                                // Header
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(); // Product
                                    columns.ConstantColumn(60); // Quantity
                                    columns.ConstantColumn(60); // Price
                                    columns.ConstantColumn(80); // Total
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Element(CellStyle).Text("Product").Bold();
                                    header.Cell().Element(CellStyle).Text("Quantity").Bold();
                                    header.Cell().Element(CellStyle).Text("Price").Bold();
                                    header.Cell().Element(CellStyle).Text("All Price").Bold();

                                    static IContainer CellStyle(IContainer container)
                                    {
                                        return container
                                            .DefaultTextStyle(x => x.FontSize(12))
                                            .Padding(5)
                                            .Background(Colors.Grey.Lighten3)
                                            .AlignCenter();
                                    }
                                });

                                // Rows
                                foreach (var detail in sellDetails)
                                {
                                    var product = products.FirstOrDefault(p => p.Id == detail.ProductId);
                                    var productName = product?.Name ?? "غير معروف";
                                    var total = detail.Quantity * detail.Price;


                                    table.Cell().Element(CellStyle).Text(productName);
                                    table.Cell().Element(CellStyle).Text(detail.Quantity.ToString());
                                    table.Cell().Element(CellStyle).Text(detail.Price.ToString("0.##"));
                                    table.Cell().Element(CellStyle).Text(total.ToString("0.##"));

                                    static IContainer CellStyle(IContainer container)
                                    {
                                        return container
                                            .DefaultTextStyle(x => x.FontSize(12))
                                            .Padding(5)
                                            .AlignCenter();
                                    }
                                }
                            });
                        }
                    });
                });
            });


            document.GeneratePdf(stream);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", "SellReport.pdf");
        }


        [HttpGet("OneSellReportPdf/{SellId}")]
        public IActionResult OneSellReport(int SellId)
        {
            var customerId = _context.Sell.Where(s => s.Id == SellId).Select(s => s.CustomerId).FirstOrDefault();
            if (customerId == null)
                return NotFound("Customer Not Found");
            var customer = _context.Customer.Where(c=>c.Id== customerId).Select(s=>s.Name).FirstOrDefault();
            if (customer == null)
                return NotFound("Customer Not Found");

            var sells = _context.Sell
                .Where(s => s.Id == SellId)
                .FirstOrDefault();


            var details = _context.Sell_Detail
                .Where(sd => SellId == sd.SellId)
                .ToList();

            var productIds = details.Select(d => d.ProductId).Distinct().ToList();

            var products = _context.Product
                .Where(p => productIds.Contains(p.Id))
                .ToList();

            var stream = new MemoryStream();

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Content().Column(col =>
                    {
                        col.Item().PaddingBottom(10).Text($"Sell Report for: {customer}")
                            .FontSize(20).Bold().AlignCenter();

                            var sellDetails = details.Where(d => d.SellId == sells.Id).ToList();

                            col.Item().PaddingVertical(10).Text($"Invoes Id: {SellId} | Date: {sells.BillDate:yyyy/MM/dd} | المبلغ الكلي: {sells.Price}")
                                .FontSize(14).Bold().FontColor(Colors.Blue.Darken1);

                            col.Item().Table(table =>
                            {
                                // Header
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(); // Product
                                    columns.ConstantColumn(60); // Quantity
                                    columns.ConstantColumn(60); // Price
                                    columns.ConstantColumn(80); // Total
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Element(CellStyle).Text("Product").Bold();
                                    header.Cell().Element(CellStyle).Text("Quantity").Bold();
                                    header.Cell().Element(CellStyle).Text("Price").Bold();
                                    header.Cell().Element(CellStyle).Text("All Price").Bold();

                                    static IContainer CellStyle(IContainer container)
                                    {
                                        return container
                                            .DefaultTextStyle(x => x.FontSize(12))
                                            .Padding(5)
                                            .Background(Colors.Grey.Lighten3)
                                            .AlignCenter();
                                    }
                                });

                                // Rows
                                foreach (var detail in sellDetails)
                                {
                                    var product = products.FirstOrDefault(p => p.Id == detail.ProductId);
                                    var productName = product?.Name ?? "غير معروف";
                                    var total = detail.Quantity * detail.Price;


                                    table.Cell().Element(CellStyle).Text(productName);
                                    table.Cell().Element(CellStyle).Text(detail.Quantity.ToString());
                                    table.Cell().Element(CellStyle).Text(detail.Price.ToString("0.##"));
                                    table.Cell().Element(CellStyle).Text(total.ToString("0.##"));

                                    static IContainer CellStyle(IContainer container)
                                    {
                                        return container
                                            .DefaultTextStyle(x => x.FontSize(12))
                                            .Padding(5)
                                            .AlignCenter();
                                    }
                                }
                            });
                        
                    });
                });
            });


            document.GeneratePdf(stream);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", "SellReport.pdf");
        }


        [HttpGet("PaymentReportPdf/{customerId}")]
        public IActionResult GeneratePaymentReport(int customerId)
        {
            var customer = _context.Customer.FirstOrDefault(c => c.Id == customerId);
            if (customer == null)
                return NotFound("Customer Not Found");

            var Payments = _context.Cus_Pay
                .Where(s => s.CusId == customerId)
                .ToList();

            var PaymentIds = Payments.Select(s => s.Id).ToList();

            var stream = new MemoryStream();

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Content().Column(col =>
                    {
                        col.Item().PaddingBottom(10).Text($"ID: {customer.Id} | Name: {customer.Name} ")
                            .FontSize(20).Bold().AlignStart();
                        col.Item().Table(table =>
                            {
                                // Header
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(30); //ID
                                    columns.ConstantColumn(60); // IQD
                                    columns.ConstantColumn(60); // USD
                                    columns.RelativeColumn(); // Note
                                    columns.RelativeColumn(); // Date
                                    columns.RelativeColumn(); // Update
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Element(CellStyle).Text("ID").Bold();
                                    header.Cell().Element(CellStyle).Text("IQD").Bold();
                                    header.Cell().Element(CellStyle).Text("USD").Bold();
                                    header.Cell().Element(CellStyle).Text("Note").Bold();
                                    header.Cell().Element(CellStyle).Text("Date").Bold();
                                    header.Cell().Element(CellStyle).Text("Update").Bold();

                                    static IContainer CellStyle(IContainer container)
                                    {
                                        return container
                                            .DefaultTextStyle(x => x.FontSize(12))
                                            .Padding(5)
                                            .Background(Colors.Grey.Lighten3)
                                            .AlignCenter();
                                    }
                                });

                                // Rows
                                foreach (var Payment in Payments)
                                {
                                    table.Cell().Element(CellStyle).Text(Payment.Id.ToString());
                                    table.Cell().Element(CellStyle).Text(Payment.MainAmount.ToString("0.##"));
                                    table.Cell().Element(CellStyle).Text(Payment.sec_Amount.ToString("0.##"));
                                    table.Cell().Element(CellStyle).Text(Payment.Note?.ToString());
                                    table.Cell().Element(CellStyle).Text(Payment.AddDate.ToString("yyyy/MM/dd"));
                                    table.Cell().Element(CellStyle).Text(Payment.ModDate.ToString("yyyy/MM/dd"));

                                    static IContainer CellStyle(IContainer container)
                                    {
                                        return container
                                            .DefaultTextStyle(x => x.FontSize(12))
                                            .Padding(5)
                                            .AlignCenter();
                                    }
                                }
                            });
                        
                    });
                });
            });


            document.GeneratePdf(stream);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", "SellReport.pdf");
        }




        [HttpGet("OnePaymentReportPdf/{payId}")]
        public IActionResult OnePaymentReport(int payId)
        {
            var customerId = _context.Cus_Pay.Where(p=>p.Id==payId).Select(c=>c.CusId).FirstOrDefault();
            var customer = _context.Customer.FirstOrDefault(c=>c.Id==customerId);
            if (customer == null)
                return NotFound("Customer Not Found");
            var Payment = _context.Cus_Pay
                .Where(p => p.Id == payId)
                .FirstOrDefault();

            var stream = new MemoryStream();

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Content().Column(col =>
                    {
                        col.Item().PaddingBottom(10).Text($"ID: {customer.Id} | Name: {customer.Name} ")
                            .FontSize(20).Bold().AlignStart();
                        col.Item().Table(table =>
                        {
                            // Header
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(30); //ID
                                columns.ConstantColumn(60); // IQD
                                columns.ConstantColumn(60); // USD
                                columns.RelativeColumn(); // Note
                                columns.RelativeColumn(); // Date
                                columns.RelativeColumn(); // Update
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("ID").Bold();
                                header.Cell().Element(CellStyle).Text("IQD").Bold();
                                header.Cell().Element(CellStyle).Text("USD").Bold();
                                header.Cell().Element(CellStyle).Text("Note").Bold();
                                header.Cell().Element(CellStyle).Text("Date").Bold();
                                header.Cell().Element(CellStyle).Text("Update").Bold();

                                static IContainer CellStyle(IContainer container)
                                {
                                    return container
                                        .DefaultTextStyle(x => x.FontSize(12))
                                        .Padding(5)
                                        .Background(Colors.Grey.Lighten3)
                                        .AlignCenter();
                                }
                            });

                              table.Cell().Element(CellStyle).Text(Payment.Id.ToString());
                              table.Cell().Element(CellStyle).Text(Payment.MainAmount.ToString("0.##"));
                              table.Cell().Element(CellStyle).Text(Payment.sec_Amount.ToString("0.##"));
                              table.Cell().Element(CellStyle).Text(Payment.Note?.ToString());
                              table.Cell().Element(CellStyle).Text(Payment.AddDate.ToString("yyyy/MM/dd"));
                              table.Cell().Element(CellStyle).Text(Payment.ModDate.ToString("yyyy/MM/dd"));

                              static IContainer CellStyle(IContainer container)
                              {
                                  return container
                                      .DefaultTextStyle(x => x.FontSize(12))
                                      .Padding(5)
                                      .AlignCenter();
                              }
                            
                        });

                    });
                });
            });


            document.GeneratePdf(stream);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", "PaymentReport.pdf");
        }
    }
}
