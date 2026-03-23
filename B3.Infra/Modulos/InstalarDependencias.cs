using B3.Dominio.Interfaces;
using B3.Infra.Contextos;
using B3.Infra.Repositorio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class InstalarDependencias
{
    public static void AdicionarBancoDeDados(this IServiceCollection servicos, IConfiguration configuracoes)
    {
        var connectionString = configuracoes.GetConnectionString("DefaultConnection");
        servicos.AddDbContext<Contexto>(options => options.UseSqlServer(connectionString));
        servicos.AddScoped<IRepositorioBase, RepositorioBase>();
    }
}