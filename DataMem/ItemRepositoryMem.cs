using RazorCRUDUI.Models;
using RazorRepoUI.Data;

namespace DataMem
{
    public class ItemRepositoryMem : IItemRepository
    {
        IList<ItemModel> _list;

        // constructor
        // init our list with our starting items
        public ItemRepositoryMem()
        {
            _list = new List<ItemModel>
        {
            new ItemModel { ID = 1, Name = "Item 1",
            Description = "Description 1", Price = 1.99m },

            new ItemModel { ID = 2, Name = "Item 2",
            Description = "Description 2", Price = 2.99m },

            new ItemModel { ID = 3, Name = "Item 3",
            Description = "Description 3", Price = 3.99m },

            new ItemModel { ID = 4, Name = "Item 4",
            Description = "Description 4", Price = 4.99m },

            new ItemModel { ID = 5, Name = "Item 5",
            Description = "Description 5", Price = 5.99m }
        };
        }

        public Task<IEnumerable<ItemModel>> GetAllItemsAsync()
        {
            return Task.FromResult(_list.AsEnumerable());
        }

        public Task<IEnumerable<ItemModel>> GetItemsAsync(string? searchString)
        {
            if (string.IsNullOrEmpty(searchString))
                return GetAllItemsAsync();
            return Task.FromResult(_list.Where(i => i.Name.Contains(searchString)));
        }

        public Task<ItemModel> InsertItemAsync(ItemModel item)
        {
            //Generate next ID
            item.ID = _list.Max(x => x.ID) + 1;

            _list.Add(item);
            return Task.FromResult(item);
        }

        public Task<bool> DeleteItemAsync(int id)
        {
            // find the item
            // return false if you can not
            var item = _list.FirstOrDefault(x => x.ID == id);
            if (item == null)
                return Task.FromResult(false);
            // we found item so delete it            		    
            _list.Remove(item);
            return Task.FromResult(true);
        }

        public Task<ItemModel?> GetItemByIdAsync(int id)
        {
            return Task.FromResult(_list.FirstOrDefault(x => x.ID == id));
        }

        public Task<bool> UpdateItemAsync(ItemModel item)
        {
            // find the item
            // return false if you can not
            var existingItem = _list.FirstOrDefault(x => x.ID == item.ID);
            if (existingItem == null)
                return Task.FromResult(false);

            // we found existing item       
            // existingItem is a reference type
            // so making changes to it here WILL change it in the list as well
            existingItem.Name = item.Name;
            existingItem.Description = item.Description;
            existingItem.Price = item.Price;
            return Task.FromResult(true);
        }
    }
}
