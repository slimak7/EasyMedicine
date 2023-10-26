using MedicinesManagement.Context;
using Microsoft.EntityFrameworkCore;

namespace MedicinesManagement.Repos.MedicineATCCategory
{
    public class MedicineATCCategoryRepo : IMedicineATCCategoryRepo
    {
        private readonly AppDbContext _dbContext;

        public MedicineATCCategoryRepo(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Models.MedicineATCCategory> Add(Models.MedicineATCCategory item)
        {
            var entry = await _dbContext.MedicineATCCategories.AddAsync(item);

            await _dbContext.SaveChangesAsync();

            return entry.Entity;
        }

        public async Task<Models.MedicineATCCategory> Delete(Guid id)
        {
            var item = await GetByID(id);

            var entry = _dbContext.MedicineATCCategories.Remove(item);

            await _dbContext.SaveChangesAsync();

            return entry.Entity;
        }

        public async Task<List<Models.MedicineATCCategory>> GetAll()
        {
            return await _dbContext.MedicineATCCategories.ToListAsync();
        }

        public async Task<List<Models.MedicineATCCategory>> GetAllByCondition(Func<Models.MedicineATCCategory, bool> condition)
        {
            var list = await GetAll();

            return list.FindAll(x => condition(x));
        }

        public async Task<List<Models.MedicineATCCategory>> GetAllByCondition(Func<Models.MedicineATCCategory, bool> condition, int page, int count)
        {
            var list = await GetAll();

            var meds = list.FindAll(x => condition(x));

            int index = page == 0 ? (page * count) : (page * count) - 1;

            if (index >= meds.Count)
            {
                return new List<Models.MedicineATCCategory>();
            }
            if (index >= meds.Count - count)
            {
                count = meds.Count - count - 1;
            }

            return meds.GetRange(index, count);
        }

        public async Task<List<Models.MedicineATCCategory>> GetAllByIndex(int index, int count)
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

        public async Task<Models.MedicineATCCategory> GetByCondition(Func<Models.MedicineATCCategory, bool> condition)
        {
            var list = await GetAll();

            return list.Find(x => condition(x));
        }

        public async Task<Models.MedicineATCCategory> GetByID(Guid id)
        {
            var entry = await _dbContext.MedicineATCCategories.FindAsync(id);

            return entry;
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Models.MedicineATCCategory> Update(Models.MedicineATCCategory item)
        {
            var entry = _dbContext.MedicineATCCategories.Update(item);

            await Save();

            return entry.Entity;
        }
    }
}
