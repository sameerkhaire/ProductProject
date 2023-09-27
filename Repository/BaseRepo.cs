using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BaseRepo<TEntity> : IBaseRepo<TEntity> where TEntity : class
    {
        private readonly ProductDbContext _productContext;
        public BaseRepo(ProductDbContext productContext)
        {
            _productContext = productContext;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var entities=  _productContext.Set<TEntity>().Add(entity);
            
                         await _productContext.SaveChangesAsync();
            return entities.Entity;


        }

        public async Task<int> DeleteAsync(int id)
        {
            TEntity entity = _productContext.Set<TEntity>().Find(id);
            if (entity != null)
            {
                _productContext.Set<TEntity>().Remove(entity);
                return await _productContext.SaveChangesAsync();
            }
            return default;
        }

        public async  Task<TEntity> FindProductAsync(int id)
        {
            return await _productContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _productContext.Set<TEntity>().ToListAsync();
        }


        public async  Task<int> UpdateAsync(TEntity entity)
        {
             _productContext.Set<TEntity>().Update(entity);
           return  await _productContext.SaveChangesAsync();
        }
    }
}
