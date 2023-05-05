namespace MedicinesManagement.Dtos
{
    public class MedicineUpdateInfoDto
    {
        public Guid MedicineID { get; set; }
        public IFormFile Leaflet { get; set; }
    }
}
