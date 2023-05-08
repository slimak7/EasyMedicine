using ActiveSubstancesManagement.Context;
using ActiveSubstancesManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace ActiveSubstancesManagement.Repos
{
    public class InteractionsRepo : IInteractionsRepo
    {
        private readonly AppDbContext _appDbContext;

        public InteractionsRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Interaction> Add(Interaction item)
        {
            var entry = await _appDbContext.Interactions.AddAsync(item);

            await Save();

            return entry.Entity;
        }

        public async Task<Interaction> Delete(Guid id)
        {
            var item = await GetByID(id);
            var entry = _appDbContext.Interactions.Remove(item);
            await Save();
            return entry.Entity;
        }

        public async Task<List<Interaction>> GetAll()
        {
            return await _appDbContext.Interactions.ToListAsync();
        }

        public async Task<List<Interaction>> GetAllByCondition(Func<Interaction, bool> condition)
        {
            var list = await GetAll();

            return list.FindAll(x => condition(x));
        }

        public async Task<Interaction> GetByCondition(Func<Interaction, bool> condition)
        {
            var list = await GetAll();

            return list.Find(x => condition(x));
        }

        public async Task<Interaction> GetByID(Guid id)
        {
            var entry = await _appDbContext.Interactions.FindAsync(id);

            return entry;
        }

        public async Task Save()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Interaction> Update(Interaction item)
        {
            var entry = _appDbContext.Interactions.Update(item);

            await Save();

            return entry.Entity;
        }
    }
}
