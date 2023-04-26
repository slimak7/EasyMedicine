namespace MedicinesManagement.ResponseModels
{
    public class MedicinesListResponse : BaseResponse
    {
        public MedicinesListResponse(string errors) : base(false, errors)
        {
        }
        public MedicinesListResponse(List<MedicineResponse> medicines) : base(true, null)
        {
            Medicines = medicines;
        }

        public List<MedicineResponse> Medicines { get; set; }
    }
}
