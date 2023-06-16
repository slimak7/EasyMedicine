using MedicinesManagement.ResponseModels;

namespace MedicinesManagement.SyncDataServices
{
    public interface IHttpDataClient
    {
        Task<MedicineInteractionsResponse> GetMedicineInteractions(Guid medicineID);
    }
}
