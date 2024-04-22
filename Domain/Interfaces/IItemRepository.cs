using RazorCRUDUI.Models;

namespace RazorRepoUI.Data
{
    public interface IItemRepository
    {
        Task<IEnumerable<ItemModel>> GetItemsAsync(String? filter);

        Task<ItemModel?> GetItemByIdAsync(int id);

        Task<bool> UpdateItemAsync(ItemModel item);

        Task<bool> DeleteItemAsync(int id);

        Task<ItemModel> InsertItemAsync(ItemModel item);
    }
}
