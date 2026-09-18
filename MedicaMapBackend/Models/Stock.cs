namespace MedicaMap.Models
{
    public class Stock
    {
        public long Id { get; set; }
        public int EstablishmentId { get; set; }
        public Establishment Establishment { get; set; } = null!;
        public int MedicationId { get; set; }
        public Medication Medication { get; set; } = null!;
        public DateTime StockDate { get; set; }
        public decimal Quantity { get; set; }
    }
}