namespace MedicinesManagement.Repos.MedicineATCCategory
{
    public interface IMedicineATCCategoryRepo : IRepo<Models.MedicineATCCategory>
    {
        Task<List<Models.MedicineATCCategory>> GetAllByCondition(Func<Models.MedicineATCCategory, bool> condition, int page, int count);
    }
}
