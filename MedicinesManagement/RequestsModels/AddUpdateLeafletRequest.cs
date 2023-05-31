using System.ComponentModel.DataAnnotations;

namespace MedicinesManagement.RequestsModels
{
    public class AddUpdateLeafletRequest
    {
        public List<Guid> MedicineID { get; set; }
        public IFormFile Leaflet { get; set; }
    }
}
