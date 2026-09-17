namespace MedicaMap.DTOs
{
    public class MunicipalityDTO
    {
        public int Id { get; set; }
        public string IbgeCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty; 
        public int StateId { get; set; }
        public string StateUf { get; set; } = string.Empty;
        public string StateName { get; set; } = string.Empty;
    }
}