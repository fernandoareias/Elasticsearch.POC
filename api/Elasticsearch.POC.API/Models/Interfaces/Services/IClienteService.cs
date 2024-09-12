namespace Elasticsearch.POC.API.Models.Interfaces.Services;

public interface IClienteService
{
    Task<Cliente?> Login(string usuario, string senha);
}