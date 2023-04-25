namespace MedicinesManagement.ResponseModels
{
    public class AllMedicinesByRangeResponse : BaseResponse
    {
        public AllMedicinesByRangeResponse(string errors) : base(false, errors)
        {
        }
        public AllMedicinesByRangeResponse(List<MedicineResponse> medicines) : base(true, null)
        {
            Medicines = medicines;
        }

        public List<MedicineResponse> Medicines { get; set; }
    }
}
