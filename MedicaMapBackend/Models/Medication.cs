namespace MedicaMap.Models
{
    public class Medication
    {
        public int Id { get; set; }
        public string CatmatCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ProductType { get; set; } = string.Empty;
        public ICollection<Stock> Stocks { get; set; } = new List<Stock>();
    }
}