using ActiveSubstancesManagement.Exceptions;
using ActiveSubstancesManagement.Repos;
using ActiveSubstancesManagement.ResponseModels;
using System.Reflection;
using System.Xml;

namespace ActiveSubstancesManagement.Services
{
    public class InteractionsService : IInteractionsService
    {
        private readonly IInteractionsRepo _interactionsRepo;

        private readonly Dictionary<string, string> translationPairs = new Dictionary<string, string>();

        public InteractionsService(IInteractionsRepo interactionsRepo)
        {
            _interactionsRepo = interactionsRepo;

            var translation = Assembly.GetExecutingAssembly().GetManifestResourceStream("ActiveSubstancesManagement.XMLTranslations.Translation_PL.xml");

            string translationString = string.Empty;
            using (StreamReader reader = new StreamReader(translation))
            {
                translationString = reader.ReadToEnd();
            }
            using (XmlReader reader = XmlReader.Create(new StringReader(translationString)))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement() && reader.Name == "ActiveSubstance")
                    {
                        string name = reader.GetAttribute("Name");
                        string guid = reader.GetAttribute("SubstanceID").ToLower();

                        if (!translationPairs.ContainsKey(guid))
                        {
                            translationPairs.Add(guid, name);
                        }

                    }
                }
            }
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
                        InteractionDefaultDescription = interaction.InteractionLevel.InteractionLevelDescription,
                        SubstancePLName = translationPairs[interaction.InteractedSubstanceID.ToString()]                      
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
