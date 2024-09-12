using Elasticsearch.POC.API.Models.Common;

namespace Elasticsearch.POC.API.Models;

public class Compra : Entidade
{

    protected Compra()
    {
        
    }

    public Compra(decimal valor, Cartao cartao)
    {

        if (valor < 0) throw new ArgumentOutOfRangeException("O valor informado para a compra e invalido");

        if (cartao is null) throw new ArgumentNullException(nameof(cartao));
        
        Valor = valor;
        
        CartaoId = cartao.Id;
        Cartao = cartao;
    }

    public decimal Valor { get; private set; }
    
    public Guid CartaoId { get; private set; }
    public Cartao Cartao { get; private set; }
}