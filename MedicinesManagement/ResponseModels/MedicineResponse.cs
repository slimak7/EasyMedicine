namespace MedicinesManagement.ResponseModels
{
    public class MedicineResponse
    {
        public string MedicineID { get; set; }
        public string MedicineName { get; set; }
        public string Power { get; set; }
        public string CompanyName { get; set; }
        public List<string> ActiveSubstances { get; set; }

    }
}
