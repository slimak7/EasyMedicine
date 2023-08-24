using MedicinesManagement.Context;
using MedicinesManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicinesManagement.Repos.Medicines
{
    public class MedicinesRepo : IMedicinesRepo
    {
        private AppDbContext _appDbContext;

        public MedicinesRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Medicine> Add(Medicine item)
        {
            var entry = await _appDbContext.Medicines.AddAsync(item);

            await Save();

            return entry.Entity;
        }

        public async Task<Medicine> Delete(Guid id)
        {
            var item = await GetByID(id);
            var entry = _appDbContext.Medicines.Remove(item);

            await Save();

            return entry.Entity;
        }

        public async Task<List<Medicine>> GetAll()
        {
            return await _appDbContext.Medicines.ToListAsync();
        }

        public async Task<List<Medicine>> GetAllByCondition(Func<Medicine, bool> condition)
        {
            var list = await GetAll();

            return list.FindAll(x => condition(x));
        }

        public async Task<List<Medicine>> GetAllByIndex(int index, int count)
        {
            var list = await GetAll();

            if (index > list.Count() - 1)
            {
                return null;
            }

            int numberOfAvailableElements = list.Count() - index;

            if (count > numberOfAvailableElements)
            {
                count = numberOfAvailableElements;
            }

            return list.GetRange(index, count);
        }

        public async Task<Medicine> GetByCondition(Func<Medicine, bool> condition)
        {
            var list = await GetAll();

            return list.Find(x => condition(x));
        }

        public async Task<Medicine> GetByID(Guid id)
        {
            var entry = await _appDbContext.Medicines.FindAsync(id);

            return entry;
        }


        public async Task Save()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Medicine> Update(Medicine item)
        {
            var entry = _appDbContext.Medicines.Update(item);

            await Save();

            return entry.Entity;
        }
    }
}
