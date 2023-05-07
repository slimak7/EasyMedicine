namespace ActiveSubstancesManagement.EventProcessing
{
    public interface IEventProcessing
    {
        void ProcessEvent(string message);
    }
}
