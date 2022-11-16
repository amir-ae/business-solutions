using BusinessSolutions.Web.Application.ViewModels;
using BusinessSolutions.Web.Application.Common.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BusinessSolutions.Web.UI.Pages
{
    public class AddItemModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? Order { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Item { get; set; }

        [BindProperty]
        public ItemViewModel NewItem { get; set; } = new();

        public IActionResult OnGetAsync()
        {
            if (Item is not null)
            {
                var orderItem = Item.Deserialize(new ItemViewModel());
                if (orderItem is not null)
                {
                    NewItem = orderItem;
                }
            }

            return Page();
        }

        public IActionResult OnPostAsync()
        {
            if (ModelState.IsValid && !string.IsNullOrEmpty(Order))
            {
                var order = Order.Deserialize(new OrderViewModel()) ?? new();

                if (Item is not null)
                {
                    var orderItem = Item.Deserialize(new ItemViewModel());
                    if (orderItem is not null)
                    {
                        order.RemoveItem(orderItem);
                    }
                }
                
                order.AddItem(NewItem);
                return RedirectToPage("CreateOrder", new { order = order.Serialize() });
            }
            return Page();
        }
    }
}
