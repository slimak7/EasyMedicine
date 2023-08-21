

using System.ComponentModel.DataAnnotations;

namespace MedicinesManagement.Models
{
    public class ATCCategory
    {
        [Key]
        public Guid ATCCategoryID { get; set; }
        public string ATCCategoryName { get; set; }
        public string ATCCategoryDescription { get; set; }
    }
}
