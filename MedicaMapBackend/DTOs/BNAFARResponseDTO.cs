using System.Text.Json.Serialization;

namespace MedicaMap.DTOs
{
    public class BNAFARResponseDTO
    {
        [JsonPropertyName("parametros")]
        public List<BNAFAREstoqueDTO> Parametros { get; set; } = new();
    }
}