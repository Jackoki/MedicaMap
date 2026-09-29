using System.Text.Json.Serialization;

namespace MedicaMap.DTOs;

public class CNESResponseDTO
{
    [JsonPropertyName("estabelecimentos")]
    public List<CNESEstabelecimentoDTO> Estabelecimentos { get; set; } = [];
}