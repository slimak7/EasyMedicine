namespace ActiveSubstancesManagement.Repos
{
    public interface IRepo <T> where T : class
    {
        Task<T> GetByID(Guid id);
        Task<List<T>> GetAll();
        Task<T> GetByCondition(Func<T, bool> condition);
        Task<List<T>> GetAllByCondition(Func<T, bool> condition);
        Task<T> Add(T item);
        Task<T> Delete(Guid id);
        Task<T> Update(T item);
        Task Save();
    }
}
