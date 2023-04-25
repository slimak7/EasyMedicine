using MedicinesManagement.ResponseModels;

namespace MedicinesManagement.Services.Medicines
{
    public interface IMedicinesService
    {
        Task<AllMedicinesByRangeResponse> GetMedicinesByRange(int index, int count);
    }
}
