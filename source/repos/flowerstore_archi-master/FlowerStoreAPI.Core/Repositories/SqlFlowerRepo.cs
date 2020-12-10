using System;
using System.Collections.Generic;
using System.Linq;
using FlowerStoreAPI.Data;
using FlowerStoreAPI.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FlowerStoreAPI.api.Models;

namespace FlowerStoreAPI.Repositories
{
    public class SqlFlowerRepo : IFlowerRepo
    {
        
        private readonly FlowerContext _context;

           public SqlFlowerRepo(FlowerContext context)
        {
            _context = context;
        }

        public void CreateFlower(Flower flower)
        {
            throw new NotImplementedException();
        }


        //function called to create flowers
        public async Task CreateFlowerAsync(int ShopId, Flower flower)
        {
            await CheckStoreExists(ShopId);
            if(flower == null){
                throw new System.NotImplementedException(nameof(flower));
            }
            
            _context.Flowers.Add(flower);
        }

        public void DeleteFlower(Flower flower)
        {
            throw new NotImplementedException();
        }


        //function called to delete flowers
        public async Task DeleteFlowerAsync(int ShopId, int id)
        {
            var flower = await GetFlowerById(ShopId, id);
            if(flower == null)
            {
                throw new ArgumentNullException(nameof(flower));
            }
            _context.Flowers.Remove(flower);
        }


        //function called to get all flowers from database
        public async Task<IEnumerable<Flower>> GetAllFlowers(int ShopId)
        {
            var storeWithFlowers = await _context.Stores
            .Include(x => x.Flowers)
            .FirstOrDefaultAsync(x => x.Id == ShopId);

            if(storeWithFlowers == null){
                throw new NotFoundException();
            }
            return storeWithFlowers.Flowers;
        }

        public Task<IEnumerable<Flower>> GetAllFlowers()
        {
            throw new NotImplementedException();
        }


        //function called to get specific flower by id
        public async Task<Flower> GetFlowerById(int ShopId, int id)
        {
            await CheckStoreExists(ShopId);
            var flower = await _context.Flowers.FirstOrDefaultAsync(x => x.Id == id && x.ShopId == ShopId);
            if(flower == null)
            {
                throw new NotFoundException();
            }
             return flower;
        }

        public Task<Flower> GetFlowerById(int id)
        {
            throw new NotImplementedException();
        }


        //function called to save changes to database
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateFlower(int ShopId, Flower flower)
        {
            //nothing
        }

        public void UpdateFlower(Flower flower)
        {
            throw new NotImplementedException();
        }

        private async Task CheckStoreExists(int ShopId)
        {
            var shopCheck = await _context.Stores.FindAsync(ShopId);
            if (shopCheck == null)
            {
                throw new NotFoundException();
            }
        }

    }
}