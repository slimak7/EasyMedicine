namespace ActiveSubstancesManagement.Dtos
{
    public class LifleatAddedDto
    {
        public Guid MedicineID { get; set; }
        public IFormFile Leaflet { get; set; }
        public string EventName { get; set; }
    }
}
