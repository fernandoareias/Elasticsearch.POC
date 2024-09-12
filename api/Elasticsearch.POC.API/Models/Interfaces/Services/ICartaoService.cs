using Elasticsearch.POC.API.Models.Common;

namespace Elasticsearch.POC.API.Models.Interfaces.Services;

public interface ICartaoService
{
    Task<Cartao> ProcessarCompra(Guid cartaoId, decimal valor);
}