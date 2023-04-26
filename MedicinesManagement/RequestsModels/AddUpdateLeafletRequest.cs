namespace MedicinesManagement.RequestsModels
{
    public class AddUpdateLeafletRequest
    {
        public Guid MedicineID { get; set; }
        public byte[] Leaflet { get; set; }
    }
}
