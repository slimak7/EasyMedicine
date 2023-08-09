using MedicinesManagement.Exceptions;
using MedicinesManagement.Repos.ActiveSubstances;
using MedicinesManagement.ResponseModels;
using MedicinesManagement.SyncDataServices.Model;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace MedicinesManagement.SyncDataServices
{
    public class HttpDataClient : IHttpDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IMedicineActiveSubstancesRepo _medicineActiveSubstancesRepo;
        private readonly IActiveSubstancesRepo _substancesRepo;
        private readonly IConfiguration _configuration;

        public HttpDataClient(HttpClient httpClient, IMedicineActiveSubstancesRepo activeSubstancesRepo, IConfiguration configuration, IActiveSubstancesRepo substancesRepo)
        {
            _httpClient = httpClient;
            _medicineActiveSubstancesRepo = activeSubstancesRepo;
            _configuration = configuration;
            _substancesRepo = substancesRepo;
        }

        public async Task<MedicineInteractionsResponse> GetMedicineInteractions(Guid medicineID)
        {
            string sectionName = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "DockerDepl" : "Dev";

            var response = await _httpClient.GetAsync($"{_configuration.GetSection(sectionName)["SubstancesURI"]}Interactions/GetInteractions/{medicineID}");

            if (response.IsSuccessStatusCode)
            {
                List<Interaction> interactions = new List<Interaction>();

                var content = await response.Content.ReadFromJsonAsync<InteractionsForMedicineResponse>();

                var substancesIDs = content.Interactions.Select(x => new Guid(x.SubstanceID)).ToList();

                var substances = await _substancesRepo.GetAllByCondition(x => substancesIDs.Contains(x.SubstanceID));

                foreach (var element in content.Interactions)
                {
                    
                    interactions.Add(new Interaction
                    {
                        InteractionDescription = element.InteractionDefaultDescription,
                        InteractionName = element.InteractionName,
                        Substance = new Substance()
                        {
                            SubstanceID = element.SubstanceID,
                            SelectedLanguageName = element.SubstancePLName,
                            Name = substances.Find(x => x.SubstanceID == new Guid(element.SubstanceID))?.SubstanceName??""
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
