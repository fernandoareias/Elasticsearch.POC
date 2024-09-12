using Bogus;
using Elasticsearch.POC.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Elasticsearch.POC.API.Data;

public class ApplicationDbContext : DbContext, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }


    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Cartao> Cartoes { get; set; }
    
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("PocDatabase");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var cartoesFicticios = new Faker<Cartao>()
            .CustomInstantiator(f => new Cartao(
                numero: f.Finance.CreditCardNumber(),
                cvv: f.Finance.CreditCardCvv(),
                senha: f.Internet.Password(),
                nomeCartao: f.Name.FullName(),
                saldo: f.Finance.Amount(100, 10000)
            ))
            .Generate(10); 

        var clientesFicticios = new Faker<Cliente>()
            .CustomInstantiator(f => new Cliente(
                usuario: f.Internet.UserName(),
                senha: f.Internet.Password()
            ))
            .Generate(10);
        
        var client = new Cliente("admin", "admin123");
        clientesFicticios.Add(client);
        
        modelBuilder.Entity<Cartao>().HasData(cartoesFicticios);
        modelBuilder.Entity<Cliente>().HasData(clientesFicticios);
    }

    public Task Commit()
    {
         
            
    }
}