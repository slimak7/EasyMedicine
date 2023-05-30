using ActiveSubstancesManagement.Exceptions;
using ActiveSubstancesManagement.Repos;
using ActiveSubstancesManagement.ResponseModels;

namespace ActiveSubstancesManagement.Services
{
    public class InteractionsService : IInteractionsService
    {
        private readonly IInteractionsRepo _interactionsRepo;

        public InteractionsService(IInteractionsRepo interactionsRepo)
        {
            _interactionsRepo = interactionsRepo;
        }

        public async Task<InteractionsForMedicineResponse> GetInteractionsForMedicine(Guid MedicineID)
        {
            var interactions = await _interactionsRepo.GetAllByCondition(x => x.MedicineID == MedicineID);

            if (interactions.Any())
            {
                List<InteractionResponse> interactionResponses = new List<InteractionResponse>();

                foreach (var interaction in interactions)
                {
                    interactionResponses.Add(new InteractionResponse
                    {
                        SubstanceID = interaction.InteractedSubstanceID.ToString(),
                        InteractionName = interaction.InteractionLevel.InteractionLevelName,
                        InteractionDefaultDescription = interaction.InteractionLevel.InteractionLevelDescription

                    });
                }

                return new InteractionsForMedicineResponse(interactionResponses);
            }
            else
            {
                throw new DataAccessException("No interactions fo given MedicineID");
            }
        }
    }
}
