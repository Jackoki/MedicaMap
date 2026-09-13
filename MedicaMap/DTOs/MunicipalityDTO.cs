namespace MedicaMap.DTOs
{
    public class MunicipalityDTO
    {
        public int Id { get; set; }
        public string IbgeCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
    }
}