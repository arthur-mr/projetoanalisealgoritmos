using AlgoritmosDotNet;
using Iot.Dominio.Interfaces;

namespace Iot.Dominio.Modelos;

public class LampadaShoyuMiAdapter : ILampada
{
    private readonly LampadaShoyuMi _lampada;

    public LampadaShoyuMiAdapter(LampadaShoyuMi lampada)
    {
        _lampada = lampada;
    }

    public void IniciarUso()
    {
        _lampada.Ligar();
    }

    public void FinalizarUso()
    {
        _lampada.Desligar();
    }
}
