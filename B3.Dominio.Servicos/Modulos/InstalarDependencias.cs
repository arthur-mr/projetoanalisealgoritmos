using B3.Dominio.Interfaces;
using B3.Dominio.Servicos.Modulos;
using B3.Dominio.Servicos.Servicos;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class InstalarDependencias
{
    public static void AdicionarServicos(this IServiceCollection servicos, IConfiguration configuracoes)
    {
        servicos.AddScoped<IAcaoServico, AcaoServico>();
        servicos.AddScoped<IOrdemServico, OrdemServico>();
        servicos.AddScoped<IEmailServico, EmailServico>();
        servicos.AddScoped<IInvestidorServico, InvestidorServico>();
        servicos.AddScoped<IPublicadorMensagemServico, PublicadorMensagemServico>();
        servicos.AdicionarConfiguracoes(configuracoes);
        servicos.AdicionarBancoDeDados(configuracoes);
    }

    public static void AdicionarConfiguracoes(this IServiceCollection servicos, IConfiguration configuracoes)
    {
        servicos.AdicionarConfiguracao<ConfiguracaoEmail>(configuracoes, nameof(ConfiguracaoEmail));
        servicos.AdicionarConfiguracao<ConfiguracaoMensageria>(configuracoes, nameof(ConfiguracaoMensageria));
    }

    private static void AdicionarConfiguracao<T>(
        this IServiceCollection services,
        IConfiguration configuration,
        string secao) where T : class, new()
    {
        var instancia = new T();

        configuration.GetSection(secao).Bind(instancia);

        services.AddSingleton(instancia);
    }
}
