namespace MedicaMap.DTOs
{
    public class BNAFAREstoqueDTO
    {
        public int CodigoUf { get; set; }
        public int CodigoMunicipio { get; set; }
        public int CodigoCnes { get; set; }
        public DateTime DataPosicaoEstoque { get; set; }
        public string CodigoCatmat { get; set; } = string.Empty;
        public decimal QuantidadeEstoque { get; set; }
        public string? NumeroLote { get; set; }
        public DateTime? DataValidade { get; set; }
        public string? TipoProduto { get; set; }
        public string? DescricaoProduto { get; set; }
        public string? Municipio { get; set; }
        public string? Uf { get; set; }
        public string? RazaoSocial { get; set; }
        public string? NomeFantasia { get; set; }
    }
}
