using MedicinesManagement.Context;
using MedicinesManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicinesManagement.Repos.ActiveSubstances
{
    public class ActiveSubstancesRepo : IActiveSubstancesRepo
    {
        private AppDbContext _appDbContext;

        public ActiveSubstancesRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<MedicineActiveSubstance> Add(MedicineActiveSubstance item)
        {
            var entry = await _appDbContext.MedicineActiveSubstances.AddAsync(item);

            await Save();

            return entry.Entity;
        }

        public async Task<MedicineActiveSubstance> Delete(Guid id)
        {
            var item = await GetByID(id);
            var entry = _appDbContext.MedicineActiveSubstances.Remove(item);

            await Save();

            return entry.Entity;
        }

        public async Task<List<MedicineActiveSubstance>> GetAll()
        {
            return await _appDbContext.MedicineActiveSubstances.ToListAsync();
        }

        public async Task<List<MedicineActiveSubstance>> GetAllByCondition(Func<MedicineActiveSubstance, bool> condition)
        {
            var list = await GetAll();

            return list.FindAll(x => condition(x));
        }

        public async Task<List<MedicineActiveSubstance>> GetAllByIndex(int index, int count)
        {
            var list = await GetAll();

            int numberOfAvailableElements = list.Count() - index;

            if (count > numberOfAvailableElements)
            {
                count = numberOfAvailableElements;
            }

            return list.GetRange(index, count);
        }

        public async Task<MedicineActiveSubstance> GetByCondition(Func<MedicineActiveSubstance, bool> condition)
        {
            var list = await GetAll();

            return list.Find(x => condition(x));
        }

        public async Task<MedicineActiveSubstance> GetByID(Guid id)
        {
            var entry = await _appDbContext.MedicineActiveSubstances.FindAsync(id);

            return entry;
        }

        public async Task Save()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<MedicineActiveSubstance> Update(MedicineActiveSubstance item)
        {
            var entry = _appDbContext.MedicineActiveSubstances.Update(item);

            await Save();

            return entry.Entity;
        }
    }
}
