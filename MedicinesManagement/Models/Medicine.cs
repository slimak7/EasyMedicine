using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicinesManagement.Models
{
    public class Medicine
    {
        [Key]
        public Guid MedicineID { get; set; }
        public string MedicineName { get; set; }   
        public string? MedicineDescription { get; set; }
    }
}
