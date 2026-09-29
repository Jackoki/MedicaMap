using System.Text.Json.Serialization;

namespace MedicaMap.DTOs;

public class CNESEstabelecimentoDTO
{
    [JsonPropertyName("codigo_cnes")]
    public int CodigoCnes { get; set; }

    [JsonPropertyName("nome_razao_social")]
    public string? NomeRazaoSocial { get; set; }

    [JsonPropertyName("nome_fantasia")]
    public string? NomeFantasia { get; set; }

    [JsonPropertyName("codigo_tipo_unidade")]
    public int? CodigoTipoUnidade { get; set; }

    [JsonPropertyName("codigo_cep_estabelecimento")]
    public string? CodigoCepEstabelecimento { get; set; }

    [JsonPropertyName("endereco_estabelecimento")]
    public string? EnderecoEstabelecimento { get; set; }

    [JsonPropertyName("numero_estabelecimento")]
    public string? NumeroEstabelecimento { get; set; }

    [JsonPropertyName("bairro_estabelecimento")]
    public string? BairroEstabelecimento { get; set; }

    [JsonPropertyName("numero_telefone_estabelecimento")]
    public string? NumeroTelefoneEstabelecimento { get; set; }

    [JsonPropertyName("latitude_estabelecimento_decimo_grau")]
    public decimal? Latitude { get; set; }

    [JsonPropertyName("longitude_estabelecimento_decimo_grau")]
    public decimal? Longitude { get; set; }

    [JsonPropertyName("endereco_email_estabelecimento")]
    public string? Email { get; set; }

    [JsonPropertyName("codigo_estabelecimento_saude")]
    public string? CodigoEstabelecimentoSaude { get; set; }

    [JsonPropertyName("codigo_uf")]
    public int CodigoUf { get; set; }

    [JsonPropertyName("codigo_municipio")]
    public int CodigoMunicipio { get; set; }

    [JsonPropertyName("data_atualizacao")]
    public DateTime? DataAtualizacao { get; set; }
}