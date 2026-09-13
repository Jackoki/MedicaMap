namespace MedicaMap.DTOs
{
    public class MedicationDTO
    {
        public int Id { get; set; }
        public string CatmatCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ProductType { get; set; } = string.Empty;
    }
}