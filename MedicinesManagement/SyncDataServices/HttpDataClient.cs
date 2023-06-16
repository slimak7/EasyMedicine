using MedicinesManagement.Exceptions;
using MedicinesManagement.Repos.ActiveSubstances;
using MedicinesManagement.ResponseModels;
using MedicinesManagement.SyncDataServices.Model;

namespace MedicinesManagement.SyncDataServices
{
    public class HttpDataClient : IHttpDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IActiveSubstancesRepo _activeSubstancesRepo;

        public HttpDataClient(HttpClient httpClient, IActiveSubstancesRepo activeSubstancesRepo)
        {
            _httpClient = httpClient;
            _activeSubstancesRepo = activeSubstancesRepo;
        }

        public async Task<MedicineInteractionsResponse> GetMedicineInteractions(Guid medicineID)
        {
            var response = await _httpClient.GetAsync($"https://192.168.0.126:45455/Interactions/GetInteractions/{medicineID}");

            if (response.IsSuccessStatusCode)
            {
                List<Interaction> interactions = new List<Interaction>();

                var content = await response.Content.ReadFromJsonAsync<InteractionsForMedicineResponse>();


                foreach (var element in content.Interactions)
                {
                    var substance = await _activeSubstancesRepo.GetByCondition(x => x.ActiveSubstance.SubstanceID == new Guid(element.SubstanceID));
                    interactions.Add(new Interaction
                    {
                        InteractionDescription = element.InteractionDefaultDescription,
                        InteractionName = element.InteractionName,
                        Substance = new Substance()
                        {
                            SubstanceID = element.SubstanceID,
                            SelectedLanguageName = element.SubstancePLName,
                            Name = substance.ActiveSubstance.SubstanceName
                        }
                    });
                }


                return new MedicineInteractionsResponse(interactions.ToArray());
            }
            else
            {

                var content = await response.Content.ReadFromJsonAsync<InteractionsForMedicineResponse>();

                throw new DataAccessException(content.Errors);

            }

        }
    }
}
