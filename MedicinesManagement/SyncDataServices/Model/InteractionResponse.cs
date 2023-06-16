namespace MedicinesManagement.SyncDataServices.Model
{
    public class InteractionResponse
    {
        public InteractionResponse()
        {
        }

        public string SubstanceID { get; set; }
        public string SubstancePLName { get; set; }
        public string InteractionName { get; set; }
        public string InteractionDefaultDescription { get; set; }
    }
}
