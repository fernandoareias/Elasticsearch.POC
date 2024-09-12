using Elasticsearch.POC.API.Eventos;
using Elasticsearch.POC.API.Models.Common;

namespace Elasticsearch.POC.API.Models;

public class Cartao : Entidade
{
    protected Cartao()
    {
        
    }

    public Cartao(string numero, string cvv, string senha, string nomeCartao, decimal saldo)
    {
        Numero = numero;
        CVV = cvv;
        Senha = senha;
        NomeCartao = nomeCartao;
        Saldo = saldo;
    }

    public string Numero { get; private set; }
    public string CVV { get; private set; }
    public string Senha { get; private set; }
    public string NomeCartao { get; private set; }
    public decimal Saldo { get; private set; }

    public Guid ClienteId { get; private set; }
    public Cliente Cliente { get; private set; }

    private List<Compra> _compras = new List<Compra>();
    public IReadOnlyCollection<Compra> Compras => _compras;

    public void ProcessarCompra(Compra compra)
    {
        if (compra is null) throw new ArgumentNullException(nameof(compra));
        
        Saldo -= compra.Valor;
        
        _compras.Add(compra);
        
        AdicionarEvento(new CompraRealizadaEvent(this, compra));
    }
}