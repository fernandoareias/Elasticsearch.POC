using Elasticsearch.POC.API.Data;
using Elasticsearch.POC.API.Models.Common;

namespace Elasticsearch.POC.API.Models.Interfaces;

public interface IBaseRepository<T> where T : Entidade
{
    Task<T> Obter(Guid id);
    Task Criar(T entity);
    Task Atualizar(T entity);
    Task Remover(T entity);
    IUnitOfWork UnitOfWork { get; }
}