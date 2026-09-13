namespace MedicaMap.DTOs
{
    public class StockLotDTO
    {
        public long Id { get; set; }
        public long StockId { get; set; }
        public string? LotNumber { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}