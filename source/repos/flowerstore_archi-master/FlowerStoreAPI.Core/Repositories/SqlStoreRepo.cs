using System;
using System.Collections.Generic;
using System.Linq;
using FlowerStoreAPI.Data;
using FlowerStoreAPI.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FlowerStoreAPI.Repositories
{
    public class SqlStoreRepo : IStoreRepo
    {
        private readonly FlowerContext _context;

          public SqlStoreRepo(FlowerContext context)
        {
            _context = context; 
        }

        public void CreateStore(Store store)
        {
            if(store == null)
            {
                throw new ArgumentNullException(nameof(store));
            }

            _context.Stores.Add(store);
        }

        public void DeleteStore(Store store)
        {
            if(store == null)
            {
                throw new ArgumentNullException(nameof(store));
            }

            _context.Stores.Remove(store);
        }

        public async Task<IEnumerable<Store>> GetAllStores()
        {
            return await _context.Stores.ToListAsync();
        }

        public async Task<Store> GetStoreById(int id)
        {
            return await _context.Stores.FirstOrDefaultAsync(p => p.Id == id);
        }

        public bool SaveChanges()
        {
             return (_context.SaveChanges() >= 0);
        }

        public void UpdateStore(Store store)
        {
            //nothing
        }
    }
}