using System.ComponentModel.DataAnnotations;

namespace MedicinesManagement.Models
{
    public class MedicineATCCategory
    {
        [Key]
        public Guid ConnectionID { get; set; }
        public string ATCFullCategory { get; set; }
        public Guid MedicineID { get;set; }
        public virtual ATCCategory ATCCategory { get; set; }
    }
}
