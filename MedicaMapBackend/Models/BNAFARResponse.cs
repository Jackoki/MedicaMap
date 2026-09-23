using MedicaMap.DTOs;

namespace MedicaMap.Models
{
    public class BNAFARResponse
    {
        public List<BNAFAREstoqueDTO> Parametros { get; set; } = new();
    }
}