using System.ComponentModel.DataAnnotations;

namespace MedicinesManagement.Models
{
    public class ActiveSubstance
    {
        [Key]
        public Guid SubstanceID { get; set; }
        public string SubstanceName { get; set; }
    }
}
