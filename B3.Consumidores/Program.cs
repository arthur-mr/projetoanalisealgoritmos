using B3.Consumidores.Consumidores;
using B3.Dominio.Servicos.Modulos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

System.Net.ServicePointManager.ServerCertificateValidationCallback =
    (sender, certificate, chain, sslPolicyErrors) => true;

await Host.CreateDefaultBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        services.AddHostedService<PrecoAcaoAlteradoConsumidor>();
        services.AdicionarServicos(ctx.Configuration);
    })
    .RunConsoleAsync();