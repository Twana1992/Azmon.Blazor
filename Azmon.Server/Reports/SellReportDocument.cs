using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Azmon.Server.Reports
{
    public class SellReportItem
    {
        public string ProductName { get; set; } = "";
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal AllPrice => Quantity * Price;
    }

    public class SellReportDocument : IDocument
    {
        public List<SellReportItem> Items { get; set; }
        public DateTime BillDate { get; set; }

        public SellReportDocument(List<SellReportItem> items, DateTime billDate)
        {
            Items = items;
            BillDate = billDate;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(20);
                page.Content().Column(column =>
                {
                    column.Item().Text($"تقرير مبيعات - التاريخ: {BillDate:yyyy/MM/dd}")
                          .FontSize(20).Bold().AlignCenter();

                    column.Item().PaddingVertical(10);

                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.ConstantColumn(60);
                            columns.ConstantColumn(80);
                            columns.ConstantColumn(100);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("المنتج").Bold();
                            header.Cell().Text("الكمية").Bold();
                            header.Cell().Text("السعر").Bold();
                            header.Cell().Text("الإجمالي").Bold();
                        });

                        foreach (var item in Items)
                        {
                            table.Cell().Text(item.ProductName);
                            table.Cell().Text(item.Quantity.ToString());
                            table.Cell().Text(item.Price.ToString("N0"));
                            table.Cell().Text(item.AllPrice.ToString("N0"));
                        }
                    });

                    var total = Items.Sum(x => x.AllPrice);
                    column.Item().PaddingTop(10).Text($"المجموع الكلي: {total:N0}")
                          .Bold().AlignRight();
                });
            });
        }
    }
}
