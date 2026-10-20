using System.Collections.Concurrent;

namespace MedicaMap.DadosLocaisServices
{
    public class ImportProgress
    {
        public string Id { get; set; } = "";
        public string Etapa { get; set; } = "Aguardando";
        public int Percentual { get; set; }
        public bool Concluido { get; set; }
        public bool Sucesso { get; set; }
        public string? Erro { get; set; }
    }

    public class ImportProgressStore
    {
        private readonly ConcurrentDictionary<string, ImportProgress> _imports = new();

        public void Atualizar(string id, string etapa, int percentual)
        {
            _imports.AddOrUpdate(id, _ => new ImportProgress
                {
                    Id = id,
                    Etapa = etapa,
                    Percentual = percentual
                },
                (_, atual) =>
                {
                    atual.Etapa = etapa;
                    atual.Percentual = percentual;
                    return atual;
                });
        }

        public void Concluir(string id)
        {
            if (_imports.TryGetValue(id, out var progresso))
            {
                progresso.Etapa = "Importação concluída";
                progresso.Percentual = 100;
                progresso.Concluido = true;
                progresso.Sucesso = true;
            }
        }

        public void Falhar(string id, string erro)
        {
            if (_imports.TryGetValue(id, out var progresso))
            {
                progresso.Etapa = "Falha na importação";
                progresso.Concluido = true;
                progresso.Sucesso = false;
                progresso.Erro = erro;
            }
        }

        public ImportProgress? Consultar(string id)
        {
            return _imports.TryGetValue(id, out var progresso) ? progresso : null;
        }
    }
}
