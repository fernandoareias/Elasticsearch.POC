using Elasticsearch.POC.API.Models;
using Elasticsearch.POC.API.Models.Interfaces;
using Elasticsearch.POC.API.Models.Interfaces.Services;

namespace Elasticsearch.POC.API.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _repository;

    public ClienteService(IClienteRepository repository)
    {
        _repository = repository;
    }

    
    
    public async Task<Cliente?> Login(string usuario, string senha)
    {
        return await _repository.Obter(usuario, senha);
    }
}