using MedicaMap.DTOs;

namespace MedicaMap.Services;

public class CnesApiService
{
    private readonly HttpClient _httpClient;

    public CnesApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async IAsyncEnumerable<CNESEstabelecimentoDTO> ObterEstabelecimentosAsync(int codigoUf, int codigoMunicipio)
    {
        const int limit = 20;
        int offset = 0;

        while (true)
        {
            var url = $"cnes/estabelecimentos" + $"?codigo_uf={codigoUf}" + $"&codigo_municipio={codigoMunicipio}" + $"&status=1" + $"&limit={limit}" + $"&offset={offset}";
            var resposta = await _httpClient.GetFromJsonAsync<CNESResponseDTO>(url);

            if (resposta == null || resposta.Estabelecimentos.Count == 0)
            {
                yield break;
            }

            foreach (var estabelecimento in resposta.Estabelecimentos)
            {
                yield return estabelecimento;
            }

            if (resposta.Estabelecimentos.Count < limit)
            {
                yield break;
            }

            offset += limit;
        }
    }

    public async Task<CNESEstabelecimentoDTO?> ObterEstabelecimentoAsync(int codigoCnes)
    {
        return await _httpClient.GetFromJsonAsync<CNESEstabelecimentoDTO>($"cnes/estabelecimentos/{codigoCnes}");
    }
}