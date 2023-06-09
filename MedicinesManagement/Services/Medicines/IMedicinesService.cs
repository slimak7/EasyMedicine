﻿using MedicinesManagement.ResponseModels;

namespace MedicinesManagement.Services.Medicines
{
    public interface IMedicinesService
    {
        Task<MedicinesListResponse> GetMedicinesByRange(int index, int count);
        Task<MedicinesListResponse> GetMedicinesByName(string name);
        Task AddUpdateLeaflet(List<Guid> medicineID, IFormFile leaflet);
    }
}
