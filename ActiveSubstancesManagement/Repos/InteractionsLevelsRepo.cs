using ActiveSubstancesManagement.Context;
using ActiveSubstancesManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace ActiveSubstancesManagement.Repos
{
    public class InteractionsLevelsRepo : IInteractionsLevelsRepo
    {
        private readonly AppDbContext _appDbContext;

        public InteractionsLevelsRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task<InteractionLevel> Add(InteractionLevel item)
        {
            throw new NotImplementedException();
        }

        public Task<InteractionLevel> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<InteractionLevel>> GetAll()
        {
            return await _appDbContext.InteractionsLevels.ToListAsync();
        }

        public Task<List<InteractionLevel>> GetAllByCondition(Func<InteractionLevel, bool> condition)
        {
            throw new NotImplementedException();
        }

        public Task<InteractionLevel> GetByCondition(Func<InteractionLevel, bool> condition)
        {
            throw new NotImplementedException();
        }

        public Task<InteractionLevel> GetByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }

        public Task<InteractionLevel> Update(InteractionLevel item)
        {
            throw new NotImplementedException();
        }
    }
}
