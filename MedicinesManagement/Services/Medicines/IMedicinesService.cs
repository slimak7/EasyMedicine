using MedicinesManagement.ResponseModels;

namespace MedicinesManagement.Services.Medicines
{
    public interface IMedicinesService
    {
        Task<MedicinesListResponse> GetMedicinesByRange(int index, int count);
        Task<MedicinesListResponse> GetMedicinesByName(string name);
        Task<MedicinesListResponse> GetMedicinesBySubstance (Guid substanceID);
        Task<MedicinesListResponse> GetSimilarMedicines(Guid medicineID, int page, int count);
        Task AddUpdateLeaflet(List<Guid> medicineID, byte[] leaflet);
    }
}
