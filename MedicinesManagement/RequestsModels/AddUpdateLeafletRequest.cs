using System.ComponentModel.DataAnnotations;

namespace MedicinesManagement.RequestsModels
{
    public class AddUpdateLeafletRequest
    {
        public Guid MedicineID { get; set; }
        public IFormFile Leaflet { get; set; }
    }
}
