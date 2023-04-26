using System.Collections.Generic;

namespace MedicinesManagement.Repos
{
    public interface IRepo <T>
    {
        Task<T> GetByID(Guid id);
        Task<List<T>> GetAll();
        Task<List<T>> GetAllByIndex(int index, int count);
        Task<T> GetByCondition(Func<T, bool> condition);
        Task<List<T>> GetAllByCondition(Func<T, bool> condition);
        Task<T> Add(T item);
        Task<T> Delete(Guid id);
        Task<T> Update(T item);
        Task Save();
    }
}
