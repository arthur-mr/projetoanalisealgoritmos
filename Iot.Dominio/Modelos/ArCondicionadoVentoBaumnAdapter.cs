using AlgoritmosDotNet;
using Iot.Dominio.Interfaces;

namespace Iot.Dominio.Modelos;

public class ArCondicionadoVentoBaumnAdapter : IArCondicionado
{
    private readonly ArCondicionadoVentoBaumn _ar;

    public ArCondicionadoVentoBaumnAdapter(ArCondicionadoVentoBaumn ar)
    {
        _ar = ar;
    }

    public void IniciarUso()
    {
        _ar.Ligar();
    }

    public void FinalizarUso()
    {
        _ar.Desligar();
    }

    public void AumentarTemperatura()
    {
        if (!_ar.EstaLigado())
            _ar.Ligar();

        _ar.DefinirTemperatura(_ar.GetTemperatura() + 1);
    }

    public void DiminuirTemperatura()
    {
        if (!_ar.EstaLigado())
            _ar.Ligar();

        _ar.DefinirTemperatura(_ar.GetTemperatura() - 1);
    }

    public void DefinirTemperatura(int temperatura)
    {
        if (!_ar.EstaLigado())
            _ar.Ligar();

        _ar.DefinirTemperatura(temperatura);
    }

    public int TemperaturaAtual => _ar.GetTemperatura();
}
