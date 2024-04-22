using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorCRUDUI.Models;
using RazorRepoUI.Data;

namespace RazorCRUDUI.Pages.Items
{
    public class DetailsModel : PageModel
    {
        private readonly IItemRepository _repo;

        public DetailsModel(IItemRepository repo)
        {
            _repo = repo;
        }

        public ItemModel ItemModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemmodel = await _repo.GetItemByIdAsync(id.Value);
            if (itemmodel == null)
            {
                return NotFound();
            }
            else
            {
                ItemModel = itemmodel;
            }
            return Page();
        }

    }
}
