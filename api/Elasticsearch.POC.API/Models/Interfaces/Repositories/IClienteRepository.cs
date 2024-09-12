namespace Elasticsearch.POC.API.Models.Interfaces;

public interface IClienteRepository : IBaseRepository<Cliente>
{
    Task<Cliente?> Obter(string usuario, string senha);
}