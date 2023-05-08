using System.ComponentModel.DataAnnotations;

namespace ActiveSubstancesManagement.Models
{
    public class InteractionLevel
    {
        [Key]
        public Guid InteractionLevelID { get; set; }
        public string InteractionLevelName { get; set; } = string.Empty;
        public string InteractionLevelDescription { get; set;} = string.Empty;
    }
}
