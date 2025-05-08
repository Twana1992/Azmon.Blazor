using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Azmon.Client.Pages
{
    public partial class Customer
    {
        private List<Core.Customer>? customer;
        private string searchQuery = string.Empty;
        private bool formState = false;
        private Core.Customer currentCustomer = new (); // used for Add/Edit
        [Inject]
        public IJSRuntime JS { get; set; } = default!;


        protected override async Task OnInitializedAsync()
        {
            customer = await CustomerService.GetAllAsync();
        }
        void NavigateToDetail(int id)
        {
            Navigation.NavigateTo($"/customer-detail/{id}");
        }

        private void ChangeState()
        {
            formState = !formState;

            if (formState) // عند الفتح
            {
                currentCustomer = new Core.Customer(); // تهيئة النموذج
            }
            else
            {
                CancelEdit(); // عند الإغلاق
            }
        }

        private async Task SearchCustomers()
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
                customer = await CustomerService.GetAllAsync();
            else
                customer = await CustomerService.SearchAsync(searchQuery);
        }


        private async Task DeleteCustomers(int id)
        {
            bool confirmed = await JS.InvokeAsync<bool>("confirm", "هل أنت متأكدة من الحذف؟");
            if (confirmed)
            {
                await CustomerService.DeleteAsync(id);
                customer = await CustomerService.GetAllAsync();
            }
        }


        private async Task SaveCustomers()
        {
            if (currentCustomer.Id == 0)
            {
                currentCustomer.MainAmount = 0;
                currentCustomer.sec_Amount = 0;
                currentCustomer.AddedDate = DateTime.Now;
                currentCustomer.UpdateDate = DateTime.Now;
                await CustomerService.CreateAsync(currentCustomer);
            }
            else
            {
                currentCustomer.UpdateDate = DateTime.Now;
                await CustomerService.UpdateAsync(currentCustomer.Id, currentCustomer);
            }

            currentCustomer = new Core.Customer();
            customer = await CustomerService.GetAllAsync();
            formState = false;
        }


        private void LoadForEdit(Core.Customer cus)
        {
            currentCustomer = new Core.Customer
            {
                Id = cus.Id,
                Name = cus.Name,
                MainAmount = cus.MainAmount,
                sec_Amount = cus.sec_Amount,
                Phone = cus.Phone,
                Address = cus.Address,
                Note = cus.Note,
                AddedDate = cus.AddedDate,
                UpdateDate = DateTime.Now

            }; ChangeState();
        }

        private void CancelEdit()
        {
            currentCustomer = new Core.Customer(); // clear form
        }

    }
}
