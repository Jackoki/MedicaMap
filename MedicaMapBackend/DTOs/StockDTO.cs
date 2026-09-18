namespace MedicaMap.DTOs
{
    public class StockDTO
    {
        public long Id { get; set; }

        public int EstablishmentId { get; set; }
        public string? EstablishmentName { get; set; }

        public int MedicationId { get; set; }
        public string? MedicationDescription { get; set; }
        public string? CatmatCode { get; set; }

        public DateTime StockDate { get; set; }
        public decimal Quantity { get; set; }
    }
}