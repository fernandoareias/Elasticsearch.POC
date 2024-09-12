namespace Elasticsearch.POC.API.DTOs;

public record CompraRequest(int numeroCartao, int cvv, string senha);