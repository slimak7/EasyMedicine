using ActiveSubstancesManagement.Dtos;
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
        public void ProcessEvent(string message)
        {
            var eType = DetermineEvent(message);

            switch (eType)
            {
                case EventType.Undefined: break;

                case EventType.LeafletAdded:

                    //TODO: leaflet processing here

                    break;
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
