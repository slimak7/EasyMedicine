using System.ComponentModel.DataAnnotations;

namespace MedicinesManagement.Models
{
    public class MedicineData
    {
        [Key]
        public Guid DataID { get; set; }
        public byte[]? leafletData { get; set; }
        public DateTime? leafletUpdateDate { get; set; }
    }
}
