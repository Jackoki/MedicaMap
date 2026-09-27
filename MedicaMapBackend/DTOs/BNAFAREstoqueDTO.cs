using System.Text.Json.Serialization;

namespace MedicaMap.DTOs
{
    public class BNAFAREstoqueDTO
    {
        [JsonPropertyName("codigo_uf")]
        public int CodigoUf { get; set; }

        [JsonPropertyName("codigo_municipio")]
        public int CodigoMunicipio { get; set; }

        [JsonPropertyName("codigo_cnes")]
        public int CodigoCnes { get; set; }

        [JsonPropertyName("data_posicao_estoque")]
        public DateTime DataPosicaoEstoque { get; set; }

        [JsonPropertyName("codigo_catmat")]
        public string CodigoCatmat { get; set; } = string.Empty;

        [JsonPropertyName("quantidade_estoque")]
        public decimal QuantidadeEstoque { get; set; }

        [JsonPropertyName("numero_lote")]
        public string? NumeroLote { get; set; }

        [JsonPropertyName("data_validade")]
        public string? DataValidade { get; set; }

        [JsonPropertyName("tipo_produto")]
        public string? TipoProduto { get; set; }

        [JsonPropertyName("sigla_programa_saude")]
        public string? SiglaProgramaSaude { get; set; }

        [JsonPropertyName("descricao_programa_saude")]
        public string? DescricaoProgramaSaude { get; set; }

        [JsonPropertyName("sigla_sistema_origem")]
        public string? SiglaSistemaOrigem { get; set; }

        [JsonPropertyName("descricao_produto")]
        public string? DescricaoProduto { get; set; }

        [JsonPropertyName("municipio")]
        public string? Municipio { get; set; }

        [JsonPropertyName("uf")]
        public string? Uf { get; set; }

        [JsonPropertyName("razao_social")]
        public string? RazaoSocial { get; set; }

        [JsonPropertyName("nome_fantasia")]
        public string? NomeFantasia { get; set; }

        [JsonPropertyName("cep")]
        public string? Cep { get; set; }

        [JsonPropertyName("logradouro")]
        public string? Logradouro { get; set; }

        [JsonPropertyName("numero_endereco")]
        public string? NumeroEndereco { get; set; }

        [JsonPropertyName("bairro")]
        public string? Bairro { get; set; }

        [JsonPropertyName("telefone")]
        public string? Telefone { get; set; }

        [JsonPropertyName("latitude")]
        public decimal? Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public decimal? Longitude { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }
    }
}