namespace MedicaMap.Services;

using System.Net.Http.Json;
using MedicaMap.DTOs;
using MedicaMap.Models;

public class BnafarApiService
{
    private readonly HttpClient _httpClient;
    public BnafarApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async IAsyncEnumerable<BNAFAREstoqueDTO> ObterEstoquesAsync()
    {
        const int limit = 100;
        int offset = 0;

        while (true)
        {
            var url = $"daf/estoque-medicamentos-bnafar-horus" + $"?limit={limit}&offset={offset}";

            var resposta = await _httpClient.GetFromJsonAsync<BNAFARResponse>(url);

            if (resposta == null || resposta.Parametros.Count == 0)
            {
                yield break;
            }

            foreach (var estoque in resposta.Parametros)
            {
                yield return estoque;
            }

            if (resposta.Parametros.Count < limit)
            {
                yield break;
            }

            offset += limit;
        }
    }
}