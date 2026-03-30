using AlgoritmosDotNet;
using Iot.Dominio.Interfaces;

namespace Iot.Dominio.Modelos;

public class LampadaPhellipesAdapter : ILampada
{
    private readonly LampadaPhellipes _lampada;

    public LampadaPhellipesAdapter(LampadaPhellipes lampada)
    {
        _lampada = lampada;
    }

    public void IniciarUso()
    {
        _lampada.SetIntensidade(100);
    }

    public void FinalizarUso()
    {
        _lampada.SetIntensidade(0);
    }
}
