namespace MedicaMap.Models
{
    public class State
    {
        public int Id { get; set; }
        public string IbgeCode { get; set; } = string.Empty;
        public string Uf { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public ICollection<Municipality> Municipalities { get; set; } = new List<Municipality>();
    }
}