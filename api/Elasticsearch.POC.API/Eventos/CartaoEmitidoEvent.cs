using System.Runtime.Serialization;
using Elasticsearch.POC.API.Models;
using Elasticsearch.POC.API.Models.Common;

namespace Elasticsearch.POC.API.Eventos;

[DataContract]
public class CartaoEmitidoEvent : Evento
{

    protected CartaoEmitidoEvent() : base("ms-cartao-emitido")
    {
        
    }

    public CartaoEmitidoEvent(Cartao cartao) : base("ms-cartao-emitido")
    {
        if (cartao is null) throw new ArgumentNullException(nameof(cartao));
        
        CartaoId = cartao.Id;
        ClienteId = cartao.ClienteId;
    }

    public DateTime DataEmissao { get; private set; } = DateTime.Now;
    public Guid CartaoId { get; private set; }
    public Guid ClienteId { get; private set; }
}