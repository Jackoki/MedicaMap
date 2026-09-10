namespace MedicaMap.Models
{
    public class Municipality
    {
        public int Id { get; set; }
        public string IbgeCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public ICollection<Establishment> Establishments { get; set; } = new List<Establishment>();
    }
}