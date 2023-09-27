using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IBaseRepo<TEntity> where TEntity : class
    {
       Task<List<TEntity>> GetAllAsync();
        Task<TEntity> FindProductAsync(int id);
       Task<TEntity> AddAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> DeleteAsync(int id);



    }
}
