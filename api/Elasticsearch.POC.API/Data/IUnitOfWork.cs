namespace Elasticsearch.POC.API.Data;

public interface IUnitOfWork
{
    Task Commit();
}