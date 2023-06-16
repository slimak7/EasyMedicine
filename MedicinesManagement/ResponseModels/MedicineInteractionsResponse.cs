namespace MedicinesManagement.ResponseModels
{
    public class MedicineInteractionsResponse : BaseResponse
    {
        public MedicineInteractionsResponse(Interaction[] interactions) : base(true, null)
        {
            Interactions = interactions;
        }
        public MedicineInteractionsResponse(string error) : base(false, error)
        {
            
        }

        public Interaction[] Interactions { get; set; }
    }
}
