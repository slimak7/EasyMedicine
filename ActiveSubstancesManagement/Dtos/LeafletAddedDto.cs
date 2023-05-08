namespace ActiveSubstancesManagement.Dtos
{
    public class LeafletAddedDto
    {
        public Guid MedicineID { get; set; }
        public byte[] Leaflet { get; set; }
        public string EventName { get; set; }
    }
}
