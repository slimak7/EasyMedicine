using ActiveSubstancesManagement.ResponseModels;

namespace ActiveSubstancesManagement.Services
{
    public interface IInteractionsService
    {
        public Task<InteractionsForMedicineResponse> GetInteractionsForMedicine(Guid MedicineID);
    }
}
