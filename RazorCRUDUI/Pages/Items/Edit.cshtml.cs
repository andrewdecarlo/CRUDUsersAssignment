using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorCRUDUI.Models;
using RazorRepoUI.Data;
using UI.Utilities;

namespace RazorCRUDUI.Pages.Items
{
    public class EditModel : PageModel
    {
        private readonly IItemRepository _repo;
        private readonly IWebHostEnvironment _env;

        public EditModel(IItemRepository repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }

        [BindProperty]
        public ItemModel Item { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tempItem = await _repo.GetItemByIdAsync(id.Value);
            if (tempItem == null)
            {
                return NotFound();
            }
            Item = tempItem;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (HttpContext.Request.Form.Files.Count > 0)
            {
                FileHelper.DeleteOldImage(_env, Item);
                Item.PictureUrl = FileHelper.UploadNewImage(_env, HttpContext.Request.Form.Files[0]);
            }

            if (!await _repo.UpdateItemAsync(Item))
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }
    }
}
