using MedicaMap.DTOs;
using MedicaMap.Models;

namespace MedicaMap.Services;

public class BnafarApiService
{
    private readonly HttpClient _httpClient;

    public BnafarApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async IAsyncEnumerable<BNAFAREstoqueDTO> ObterEstoquesAsync(int? codigoUf = null, int? codigoMunicipio = null, int? codigoCnes = null, string? dataPosicaoEstoque = null, string? codigoCatmat = null)
    {
        const int limit = 1000;
        int offset = 0;

        while (true)
        {
            var parametros = new List<string>();

            if (codigoUf.HasValue)
                parametros.Add($"codigo_uf={codigoUf.Value}");

            if (codigoMunicipio.HasValue)
                parametros.Add($"codigo_municipio={codigoMunicipio.Value}");

            if (codigoCnes.HasValue)
                parametros.Add($"codigo_cnes={codigoCnes.Value}");

            if (!string.IsNullOrWhiteSpace(dataPosicaoEstoque))
                parametros.Add($"data_posicao_estoque={dataPosicaoEstoque}");

            if (!string.IsNullOrWhiteSpace(codigoCatmat))
                parametros.Add($"codigo_catmat={Uri.EscapeDataString(codigoCatmat)}");

            parametros.Add($"limit={limit}");
            parametros.Add($"offset={offset}");

            var url = "daf/estoque-medicamentos-bnafar-horus?" + string.Join("&", parametros);

            var resposta =  await _httpClient.GetFromJsonAsync<BNAFARResponse>(url);

            if (resposta == null || resposta.Parametros.Count == 0)
                yield break;

            foreach (var estoque in resposta.Parametros)
                yield return estoque;

            if (resposta.Parametros.Count < limit)
                yield break;

            offset += limit;
        }
    }
}