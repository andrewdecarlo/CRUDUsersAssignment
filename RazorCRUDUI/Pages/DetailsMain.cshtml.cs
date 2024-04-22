using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorCRUDUI.Models;
using RazorRepoUI.Data;

namespace UI.Pages
{
    [Authorize]
    public class DetailsMainModel : PageModel
    {
        private readonly IItemRepository _repo;

        public ItemModel Item { get; set; } = default!;

        public DetailsMainModel(IItemRepository repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _repo.GetItemByIdAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                Item = item;
            }
            return Page();
        }
    }
    
}
