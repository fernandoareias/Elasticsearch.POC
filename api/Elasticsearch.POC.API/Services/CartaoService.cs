using Elasticsearch.POC.API.Models;
using Elasticsearch.POC.API.Models.Interfaces;
using Elasticsearch.POC.API.Models.Interfaces.Services;

namespace Elasticsearch.POC.API.Services;

public class CartaoService : ICartaoService
{

    private readonly ICartaoRepository _repository;

    public CartaoService(ICartaoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Cartao> ProcessarCompra(Guid cartaoId, decimal valor)
    {
        var cartao = await _repository.Obter(cartaoId);

        if (cartao is null) return null;

        var compra = new Compra(valor, cartao);
        
        cartao.ProcessarCompra(compra);

        await _repository.Atualizar(cartao);
        await _repository.UnitOfWork.Commit();
        
        return cartao;
    }
}