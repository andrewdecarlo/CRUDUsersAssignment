using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorCRUDUI.Models;
using RazorRepoUI.Data;

namespace RazorCRUDUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IItemRepository _repo;
        private readonly ILogger<IndexModel> _logger;

        //Pass IItemRepository into constructor
        public IndexModel(IItemRepository repo, ILogger<IndexModel> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        //Iterable list of items in the database
        public IList<ItemModel> ItemModel { get; set; } = default!;


        //Properties for search bar
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }


        public async Task OnGetAsync()
        {
            ItemModel = (IList<ItemModel>)await _repo.GetItemsAsync(SearchString);
        }
    }
}
