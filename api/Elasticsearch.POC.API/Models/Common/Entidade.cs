namespace Elasticsearch.POC.API.Models.Common;

public abstract class Entidade
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public DateTime CriadoEm { get; private set; } = DateTime.Now;
    public DateTime? AtualizadoEm { get; private set; }

    private List<Evento> _eventos = new List<Evento>();
    public IReadOnlyCollection<Evento> Eventos => _eventos;


    protected void AdicionarEvento(Evento @evento)
    {
        _eventos.Add(@evento);
    }

    protected void LimparEventos() => _eventos.Clear();

}