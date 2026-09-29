using MedicaMap.DTOs;

namespace MedicaMap.APIServices
{
    public class IBGEAPIService
    {
        private readonly HttpClient _httpClient;

        public IBGEAPIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<IBGEEstadoDTO>> ObterEstadosAsync()
        {
            var resposta = await _httpClient.GetFromJsonAsync<List<IBGEEstadoDTO>>("api/v1/localidades/estados");
            return resposta ?? new List<IBGEEstadoDTO>();
        }

        public async Task<List<IBGEMunicipioDTO>> ObterMunicipiosAsync(int codigoUf)
        {
            var resposta = await _httpClient.GetFromJsonAsync<List<IBGEMunicipioDTO>>($"api/v1/localidades/estados/{codigoUf}/municipios");
            return resposta ?? new List<IBGEMunicipioDTO>();
        }
    }
}
