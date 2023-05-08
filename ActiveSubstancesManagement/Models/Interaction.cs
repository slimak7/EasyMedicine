using System.ComponentModel.DataAnnotations;

namespace ActiveSubstancesManagement.Models
{
    public class Interaction
    {
        [Key]
        public Guid InteractionID { get; set; }
        public Guid MedicineID { get; set; }
        public Guid InteractedSubstanceID { get; set; }
        public virtual InteractionLevel InteractionLevel { get; set; }
    }
}
