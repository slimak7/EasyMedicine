using MedicinesManagement.Context;
using MedicinesManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicinesManagement.Repos.ActiveSubstances
{
    public class ActiveSubstancesRepo : IActiveSubstancesRepo
    {
        private readonly AppDbContext _appDbContect;

        public ActiveSubstancesRepo(AppDbContext appDbContect)
        {
            _appDbContect = appDbContect;
        }

        public async Task<ActiveSubstance> Add(ActiveSubstance item)
        {
            var entry = await _appDbContect.ActiveSubstances.AddAsync(item);

            await Save();

            return entry.Entity;
        }

        public async Task<ActiveSubstance> Delete(Guid id)
        {
            var item = await GetByID(id);
            var entry = _appDbContect.ActiveSubstances.Remove(item);
            await Save();
            return entry.Entity;
        }

        public async Task<List<ActiveSubstance>> GetAll()
        {
            return await _appDbContect.ActiveSubstances.ToListAsync();
        }

        public async Task<List<ActiveSubstance>> GetAllByCondition(Func<ActiveSubstance, bool> condition)
        {
            var list = await GetAll();

            return list.FindAll(x => condition(x));
        }

        public Task<List<ActiveSubstance>> GetAllByIndex(int index, int count)
        {
            throw new NotImplementedException();
        }

        public async Task<ActiveSubstance> GetByCondition(Func<ActiveSubstance, bool> condition)
        {
            var list = await GetAll();

            return list.Find(x => condition(x));
        }

        public async Task<ActiveSubstance> GetByID(Guid id)
        {
            var entry = await _appDbContect.ActiveSubstances.FindAsync(id);

            return entry;
        }

        public async Task Save()
        {
            await _appDbContect.SaveChangesAsync();
        }

        public async Task<ActiveSubstance> Update(ActiveSubstance item)
        {
            var entry = _appDbContect.ActiveSubstances.Update(item);

            await Save();

            return entry.Entity;
        }
    }
}
