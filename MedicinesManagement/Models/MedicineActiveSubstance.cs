using System.ComponentModel.DataAnnotations;

namespace MedicinesManagement.Models
{
    public class MedicineActiveSubstance
    {
        [Key]
        public Guid ConnectionID { get; set; }

        public virtual Medicine Medicine { get; set; }
        public virtual ActiveSubstance ActiveSubstance { get; set; }
    }
}
