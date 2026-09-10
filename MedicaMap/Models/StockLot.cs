namespace MedicaMap.Models
{
    public class StockLot
    {
        public long Id { get; set; }
        public long StockId { get; set; }
        public Stock Stock { get; set; } = null!;
        public string? LotNumber { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}