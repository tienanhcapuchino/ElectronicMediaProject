using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicMedia.Core.Services.Interfaces
{
    public interface ICoreRepository<T>
    {
        Task<T> GetByIdAsync(Guid id);
        Task<List<T>> GetAllAsync();
        Task<bool> Delete(Guid id, bool saveChange = true);
        Task<bool> Update(Guid id, T entity);
        Task<bool> Add(T entity);
    }
}
