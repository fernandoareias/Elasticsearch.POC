using Elasticsearch.POC.API.Models.Common;

namespace Elasticsearch.POC.API.Models;

public class Cliente : Entidade
{
    protected Cliente()
    {
        
    }
    public Cliente(string usuario, string senha)
    {
        Usuario = usuario;
        Senha = senha;
    }

    public string Usuario { get; private set; }
    public string Senha { get; private set; }


    public List<Cartao> Cartoes { get; private set; }
 
}