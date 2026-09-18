namespace MedicaMap.DTOs
{
    public class StateDTO
    {
        public int Id { get; set; }
        public string IbgeCode { get; set; } = string.Empty;
        public string Uf { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}