using System.Runtime.Serialization;
using Elastic.CommonSchema;
using Elasticsearch.POC.API.Models;

namespace Elasticsearch.POC.API.Eventos;

[DataContract]
public class CompraRealizadaEvent : Models.Common.Evento
{

    protected CompraRealizadaEvent() : base("ms-compra-realizada")
    {
        
    }

    public CompraRealizadaEvent(Cartao cartao, Compra compra) : base("ms-compra-realizada")
    {
        if (cartao is null) throw new ArgumentNullException(nameof(cartao));

        ClienteId = cartao.ClienteId;
        CartaoId = cartao.Id;
        CompraId = compra.Id;
        Valor = compra.Valor;
    }
    
    [DataMember]
    public Guid ClienteId { get; private set; }
    
    [DataMember]
    public Guid CartaoId { get; private set; }
    
    [DataMember] 
    public Guid CompraId { get; private set; }
    
    [DataMember]
    public decimal Valor { get; private set; }
}