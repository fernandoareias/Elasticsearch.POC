using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Elasticsearch.POC.API.Models.Common;

[DataContract]
public abstract class Evento
{ 
    protected Evento(string topic)
    {
        Topic = topic;
    }

    [DataMember] public Guid Id { get; private set; } = Guid.NewGuid();
    
    [DataMember]
    public DateTime CriadoEm { get; private set; } = DateTime.Now;
    
    [JsonIgnore]
    public string Topic { get; private set; }
}