using AlgoritmosDotNet;
using Iot.Dominio.Interfaces;
using Iot.Dominio.Modelos;

namespace Microsoft.Extensions.DependencyInjection;

public static class InstalarDependencias
{
    public static void AdicionarServicos(this IServiceCollection servicos)
    {
        servicos.AdicionarBibliotecaIoT();
        servicos.AddSingleton<ILampada, LampadaPhellipesAdapter>();
        servicos.AddSingleton<ILampada, LampadaShoyuMiAdapter>();
        servicos.AddSingleton<IPersiana, PersianaSolariusAdapter>();
        servicos.AddSingleton<IPersiana, PersianaNatLightAdapter>();
        servicos.AddSingleton<IArCondicionado, ArCondicionadoVentoBaumnAdapter>();
        servicos.AddSingleton<IArCondicionado, ArCondicionadoGellaKazaAdapter>();
        servicos.AddSingleton<ICasaInteligente, CasaInteligente>();
    }

    private static void AdicionarBibliotecaIoT(this IServiceCollection servicos)
    {
        servicos.AddSingleton<LampadaPhellipes>();
        servicos.AddSingleton<LampadaShoyuMi>();
        servicos.AddSingleton<PersianaSolarius>();
        servicos.AddSingleton<PersianaNatLight>();
        servicos.AddSingleton<ArCondicionadoVentoBaumn>();
        servicos.AddSingleton<ArCondicionadoGellaKaza>();
    }
}
