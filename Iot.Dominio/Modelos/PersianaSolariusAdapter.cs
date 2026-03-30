using AlgoritmosDotNet;
using Iot.Dominio.Interfaces;

namespace Iot.Dominio.Modelos;

public class PersianaSolariusAdapter : IPersiana
{
    private readonly PersianaSolarius _persiana;

    public PersianaSolariusAdapter(PersianaSolarius persiana)
    {
        _persiana = persiana;
    }

    public void IniciarUso()
    {
        _persiana.SubirPersiana();
    }

    public void FinalizarUso()
    {
        _persiana.DescerPersiana();
    }
}
