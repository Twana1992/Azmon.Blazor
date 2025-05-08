using Azmon.Client.Service;

namespace Azmon.Client.Pages
{
    public partial class Counter
    {

        private List<Core.Customer>? customer;
        private string searchQuery = string.Empty;
        private bool formState = false;
        private Core.Customer currentCustomer = new(); // used for Add/Edit



        protected override async Task OnInitializedAsync()
        {
            customer = await CustomerService.GetAllAsync();
        }

        private void ChangeState()
        {
            if (formState == false) { formState = true; }
            else { formState = false; CancelEdit(); }

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
            await CustomerService.DeleteAsync(id);
            customer = await CustomerService.GetAllAsync();
        }

        private async Task SaveCustomers()
        {
            if (currentCustomer.Id == 0)
            {
                // Add new
                currentCustomer.MainAmount = 0;
                currentCustomer.sec_Amount = 0;
                currentCustomer.AddedDate = DateTime.Now;
                currentCustomer.UpdateDate = DateTime.Now;
                await CustomerService.CreateAsync(currentCustomer);
            }
            else
            {
                // Update existing
                await CustomerService.UpdateAsync(currentCustomer.Id, currentCustomer);
            }

            currentCustomer = new Core.Customer(); // reset form
            customer = await CustomerService.GetAllAsync(); // refresh list
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
