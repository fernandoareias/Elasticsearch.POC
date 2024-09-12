using Elasticsearch.POC.API.Models;
using Elasticsearch.POC.API.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Elasticsearch.POC.API.Data.Repositories;

public class CartaoRepository : ICartaoRepository
{
    private readonly DbContext _dbContext;

    public CartaoRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Cartao> Obter(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task Criar(Cartao entity)
    {
        throw new NotImplementedException();
    }

    public Task Atualizar(Cartao entity)
    {
        throw new NotImplementedException();
    }

    public Task Remover(Cartao entity)
    {
        throw new NotImplementedException();
    }

    public IUnitOfWork UnitOfWork => _dbContext;
}