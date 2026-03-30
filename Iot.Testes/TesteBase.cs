using Iot.Dominio.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Iot.Testes;

public abstract class TesteBase
{
    private readonly IServiceProvider ServiceProvider;

    protected TesteBase()
    {
        var servicos = new ServiceCollection();
        servicos.AdicionarServicos();
        ServiceProvider = servicos.BuildServiceProvider();
    }

    protected ICasaInteligente Casa => ServiceProvider.GetRequiredService<ICasaInteligente>();

    protected T ObterServico<T>() where T : notnull => ServiceProvider.GetRequiredService<T>();
}