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

        protected override async Task OnInitializedAsync()
        {
            customer = await CustomerService.GetByIdAsync(Id);
            cus_Pay = await Cus_PayService.GetByCIdAsync(Id);
            sell = await sellService.GetByCIdAsync(Id);
            products = await ProductService.GetAllProductsAsync();
            newSell.BillDate = DateTime.Now;
            newSell.UpdateDate = DateTime.Now;
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

            cus_Pay = await Cus_PayService.GetByCIdAsync(Id);
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

        private void AddDetail()
        {
            var detail = new Sell_Detail
            {
                ProductId = newDetail.ProductId,
                Price = newDetail.Price,
                Quantity = newDetail.Quantity
            };

            detailList.Add(detail);
            newDetail = new(); // Reset the form
        }
        private void RemoveDetail(Sell_Detail detail)
        {
            detailList.Remove(detail);
        }
        private async Task SaveSellAsync()
        {
            newSell.Sell_Detail = detailList;

            var response = await sellService.CreateAsync(newSell);
                // Reset
                newSell = new();
                detailList.Clear();
                ToggleSellForm();
            
        }

        private void ToggleSellForm()
        {
            showSellForm = !showSellForm;
        }
    }
}
