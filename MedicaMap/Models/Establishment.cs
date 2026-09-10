namespace MedicaMap.Models
{
    public class Establishment
    {
        public int Id { get; set; }
        public string CnesCode { get; set; } = string.Empty;
        public string? TradeName { get; set; }
        public string? Cep { get; set; }
        public string? Street { get; set; }
        public string? AddressNumber { get; set; }
        public string? Neighborhood { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int MunicipalityId { get; set; }
        public Municipality Municipality { get; set; } = null!;
    }
}