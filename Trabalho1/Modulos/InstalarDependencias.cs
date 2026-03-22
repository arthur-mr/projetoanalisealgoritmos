using Trabalho1.Interfaces;
using Trabalho1.Servicos;
using Trabalho1.Servicos.Calculadores;

namespace Microsoft.Extensions.DependencyInjection;

public static class InstalarDependencias
{
    public static void AdicionarServicos(this IServiceCollection services)
    {
        services.AddScoped<ICalculadorEntregaServico, CalculadorEntregasServico>();
        services.AddScoped<IPedidoServico, PedidoServico>();
        services.AddScoped<ICalculadorEntregas, CalculadorEntregaPac>();
        services.AddScoped<ICalculadorEntregas, CalculadorEntregaSedex>();
        services.AddScoped<ICalculadorEntregas, CalculadorEntregaRetiradaLocal>();
    }
}