using MedicinesManagement.ResponseModels;

namespace MedicinesManagement.SyncDataServices.Model
{
    public class InteractionsForMedicineResponse : BaseResponse
    {
        public InteractionsForMedicineResponse(List<InteractionResponse> interactions) : base(true, null)
        {
            Interactions = interactions;
        }

        public InteractionsForMedicineResponse(string errors) : base(false, errors)
        {

        }

        public InteractionsForMedicineResponse() : base(true, null)
        {
        }

        public List<InteractionResponse> Interactions { get; set; }
    }
}
