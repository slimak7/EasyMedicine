namespace ActiveSubstancesManagement.ResponseModels
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

        public List<InteractionResponse> Interactions { get; set; }
    }
}
