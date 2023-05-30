namespace ActiveSubstancesManagement.Dtos
{
    public class LeafletAddedDto
    {
        public Guid MedicineID { get; set; }
        public Guid[] SubstancesID { get; set; }
        public byte[] Leaflet { get; set; }
        public string EventName { get; set; }
    }
}
