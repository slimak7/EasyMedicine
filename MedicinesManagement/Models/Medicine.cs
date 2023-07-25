using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicinesManagement.Models
{
    public class Medicine
    {
        [Key]
        public Guid MedicineID { get; set; }
        public string MedicineName { get; set; }
        public string Power { get; set; }
        public string CompanyName { get; set; }
        public string leafletURL { get; set; }
    }
}
