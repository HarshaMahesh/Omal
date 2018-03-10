using System.Collections.Generic;
using System.Threading.Tasks;

namespace Omal
{
    public interface IDataStore<T>
    {
        Task<Models.ResponseBase> AddItemAsync(T item);
        Task<Models.ResponseBase> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(int id);
        Task<T> GetItemAsync(int id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
