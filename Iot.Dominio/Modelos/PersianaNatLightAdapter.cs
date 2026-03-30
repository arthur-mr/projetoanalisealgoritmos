using AlgoritmosDotNet;
using Iot.Dominio.Interfaces;

namespace Iot.Dominio.Modelos;

public class PersianaNatLightAdapter : IPersiana
{
    private readonly PersianaNatLight _persiana;

    public PersianaNatLightAdapter(PersianaNatLight persiana)
    {
        _persiana = persiana;
    }

    public void IniciarUso()
    {
        _persiana.AbrirPalheta();
        _persiana.SubirPalheta();
    }

    public void FinalizarUso()
    {
        _persiana.DescerPalheta();
        _persiana.FecharPalheta();
    }
}
