using ActiveSubstancesManagement.Dtos;
using ActiveSubstancesManagement.Helpers;
using ActiveSubstancesManagement.Models;
using ActiveSubstancesManagement.Repos;
using System.Diagnostics;
using System.Text.Json;

namespace ActiveSubstancesManagement.EventProcessing
{
    public class EventProcessing : IEventProcessing
    {
        private IServiceScopeFactory _scopeFactory;

        public EventProcessing(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        public async void ProcessEvent(string message)
        {
            var eType = DetermineEvent(message);

            switch (eType)
            {
                case EventType.Undefined: break;

                case EventType.LeafletAdded:

                    var item = JsonSerializer.Deserialize<LeafletAddedDto>(message);

                    if (item == null)
                    {
                        Console.WriteLine("Leaflet is null!!");
                        return;
                    }

                    ProcessLeaflet(item);
                    
                    break;
            }
        }

        private async void ProcessLeaflet(LeafletAddedDto leaflet)
        {
            var interactions = LeafletProcessing.GetInteractedSubstances(leaflet);

            using (var scope = _scopeFactory.CreateScope())
            {
                var interactionsRepo = scope.ServiceProvider.GetRequiredService<IInteractionsRepo>();
                var interactionsLevelsRepo = scope.ServiceProvider.GetRequiredService<IInteractionsLevelsRepo>();

                var interactionsLevels = await interactionsLevelsRepo.GetAll();

                foreach (var id in leaflet.MedicineID)
                {

                    var currentInteractions = await interactionsRepo.GetAllByCondition(x => x.MedicineID == id);

                    if (currentInteractions.Any())
                    {
                        foreach (var i in currentInteractions)
                        {
                            await interactionsRepo.Delete(i.InteractionID);
                        }
                    }


                    foreach (var interaction in interactions)
                    {
                        if (!leaflet.SubstancesID.Contains(interaction.substanceID))
                        {
                            await interactionsRepo.Add(new Interaction()
                            {
                                InteractionID = new Guid(),
                                MedicineID = id,
                                InteractedSubstanceID = interaction.substanceID,
                                InteractionLevel = interactionsLevels[interaction.interactionLevel - 1]
                            });
                        }
                    }

                }
                Debug.WriteLine("\n\n\n\n\n\n\n\nLeaflet for medicineID: " + leaflet.MedicineID[0] + " processed\n" + interactions.Count() + " interaction(s) added");
            }
        }

        private EventType DetermineEvent(string message)
        {
            var eventObject = JsonSerializer.Deserialize<GenericEvent>(message);

            switch (eventObject?.EventName)
            {
                case "AddUpdateLeaflet":

                    return EventType.LeafletAdded;

                default:

                    return EventType.Undefined;

            }

        }
    }

    public enum EventType
    {
        Undefined,
        LeafletAdded
    }
}
