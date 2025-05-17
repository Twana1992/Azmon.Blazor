using Azmon.Client.Service;
using Azmon.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Azmon.Client.Pages
{
    public partial class Customer_Detail
    {
        [Parameter] public int Id { get; set; }
        private Core.Customer? customer;
        private List<Core.Cus_Pay>? cus_Pay;
        private Sell newSell = new();
        private List<Core.Sell>? sell;
        private Sell_Detail newDetail = new();
        private List<Sell_Detail> detailList = new();
        private List<Product> products = new();
        private Core.Cus_Pay newPayment = new();
        bool isPaymentModalVisible = false;
        private bool editMode = false;
        private bool showSellForm = false;
        private bool editSellMode = false;
        private decimal totalPrice => detailList.Sum(d => d.Price * d.Quantity);


        protected override async Task OnInitializedAsync()
        {
            customer = await CustomerService.GetByIdAsync(Id);
            await CustomerBalanceService.CalculateAndSaveBalance(Id,customer);
            customer = await CustomerService.GetByIdAsync(Id);
            cus_Pay = await Cus_PayService.GetByCIdAsync(Id);
            sell = await sellService.GetByCIdAsync(Id);
            products = await ProductService.GetAllProductsAsync();
            newSell.BillDate = DateTime.Now;
            newSell.UpdateDate = DateTime.Now;
            newSell.CurrencyType = "IQD";
        }


        void ToggleForm()
        {
            isPaymentModalVisible = !isPaymentModalVisible;
            if (!isPaymentModalVisible)
            {
                newPayment = new();
                editMode = false;
            }
        }
        void ClosePaymentModal()
        {
            isPaymentModalVisible = false;
            newPayment = new();
            editMode = false;
        }


        async Task SavePayment()
        {
            newPayment.CusId = Id;
            newPayment.ModDate = DateTime.Now;

            if (editMode)
            {
                if (newPayment.Id > 0)
                {
                    Console.WriteLine($"Editing Payment ID: {newPayment.Id}");
                    await Cus_PayService.UpdateAsync(newPayment.Id, newPayment);
                }
                else
                {
                    Console.WriteLine("خطأ: ID غير صالح للتعديل");
                }
            }
            else
            {
                newPayment.AddDate = DateTime.Now;
                await Cus_PayService.CreateAsync(newPayment);
            }
           await CustomerBalanceService.CalculateAndSaveBalance(Id, customer);
            await JS.InvokeVoidAsync("alert", "تم تحديث الرصيد بنجاح!");
            cus_Pay = await Cus_PayService.GetByCIdAsync(Id);
            customer = await CustomerService.GetByIdAsync(Id);
            sell = await sellService.GetByCIdAsync(Id);
            ToggleForm();
        }

        void EditPayment(Core.Cus_Pay p)
        {
            newPayment = new Core.Cus_Pay
            {
                Id = p.Id,
                CusId = p.CusId,
                MainAmount = p.MainAmount,
                sec_Amount = p.sec_Amount,
                Note = p.Note,
                AddDate = p.AddDate,
                ModDate = DateTime.Now
            };
            editMode = true;
            isPaymentModalVisible = true;
        }

        async Task DeletePayment(int id)
        {
            var confirmed = await JS.InvokeAsync<bool>("confirm", "هل أنت متأكد من حذف هذه الدفعة؟");
            if (confirmed)
            {
                await Cus_PayService.DeleteAsync(id);
                cus_Pay = await Cus_PayService.GetByCIdAsync(Id);
            }
        }


        [Inject] public IJSRuntime JS { get; set; } = default!;

        private void RemoveDetail(Sell_Detail detail)
        {
            detailList.Remove(detail);
            StateHasChanged();
        }

        private void AddDetail()
        {
            if (newDetail.ProductId == 0)
                return;

            if (newDetail.Price <= 0 || newDetail.Quantity <= 0)
                return;

            var detail = new Sell_Detail
            {
                ProductId = newDetail.ProductId,
                Quantity = newDetail.Quantity,
                Price = newDetail.Price,
                AllPrice = newDetail.Price * newDetail.Quantity
            };

            detailList.Add(detail);
            newDetail = new Sell_Detail(); // إعادة التهيئة
            StateHasChanged();
        }


        private async Task SaveSellAsync()
        {
            newSell.CustomerId = Id;

            // جمع السعر الكلي للتفاصيل
            newSell.Price = detailList.Sum(d => d.Price * d.Quantity);

            if (editSellMode && newSell.Id > 0)
            {
                // تحديث تاريخ التعديل فقط
                newSell.UpdateDate = DateTime.Now;

                await sellService.UpdateAsync(newSell.Id, newSell);

                var existingDetails = await sellDetailService.GetBySellIdAsync(newSell.Id);

                foreach (var oldDetail in existingDetails)
                {
                    if (!detailList.Any(d => d.Id == oldDetail.Id))
                    {
                        await sellDetailService.DeleteAsync(oldDetail.Id);
                    }
                }

                foreach (var detail in detailList)
                {
                    detail.SellId = newSell.Id;

                    if (detail.Id > 0)
                        await sellDetailService.UpdateAsync(detail.Id, detail);
                    else
                        await sellDetailService.CreateAsync(detail);
                }
            }
            else
            {
                // عند الإضافة الجديدة: ضبط التاريخين
                newSell.BillDate = DateTime.Now;
                newSell.UpdateDate = DateTime.Now;

                var savedSell = await sellService.CreateAsync(newSell);
                if (savedSell != null && savedSell.Id > 0)
                {
                    foreach (var detail in detailList)
                    {
                        detail.SellId = savedSell.Id;
                        await sellDetailService.CreateAsync(detail);
                    }
                }
            }

            // Reset form
            sell = await sellService.GetByCIdAsync(Id);
            newSell = new();
            detailList.Clear();
            editSellMode = false;
            ToggleSellForm();
        }



        private void ToggleSellForm()
        {
            showSellForm = !showSellForm;
            if (!showSellForm)
            {
                newSell = new();
                newDetail = new();
                newSell.CurrencyType = "IQD";
                detailList.Clear();
                editSellMode = false;
            }
        }

        private async Task EditSell(Sell s)
        {
            newSell = new Sell
            {
                Id = s.Id,
                CustomerId = s.CustomerId,
                Paid = s.Paid,
                CurrencyType = s.CurrencyType,
                BillDate = s.BillDate,
                UpdateDate = DateTime.Now,
                Price = s.Price
            };

            detailList = await sellDetailService.GetBySellIdAsync(s.Id) ?? new List<Sell_Detail>();
            showSellForm = true;
            editSellMode = true;
        }


        private async Task DeleteSell(int id)
        {
            var confirmed = await JS.InvokeAsync<bool>("confirm", "هل أنت متأكدة من حذف البيع؟");
            if (confirmed)
            {
                // حذف التفاصيل المرتبطة أولاً
                var details = await sellDetailService.GetBySellIdAsync(id);
                foreach (var d in details)
                {
                    await sellDetailService.DeleteAsync(d.Id);
                }

                // حذف البيع
                await sellService.DeleteAsync(id);

                // تحديث القائمة
                sell = await sellService.GetByCIdAsync(Id);
            }
        }



        void ShowSellReport(int sellId)
        {
            // هذا إذا تريدي تفتحين صفحة جديدة للتقرير
            NavigationManager.NavigateTo($"/sell-report/{sellId}");
        }


        async Task DownloadReport(int cusID)
        {
            await ReportService.SellReportPdf(cusID);
        }
        async Task OneReport(int Id)
        {
            await ReportService.OneSellReportPdf(Id);
        }

        async Task PaymentReport(int cusID)
        {
            await ReportService.PaymentReportPdf(cusID);
        }

        async Task OnePaymentReport(int payId)
        {
            await ReportService.OnePaymentReportPdf(payId);
        }
    }
}
