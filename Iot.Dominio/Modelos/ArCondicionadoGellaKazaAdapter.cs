using AlgoritmosDotNet;
using Iot.Dominio.Interfaces;

namespace Iot.Dominio.Modelos;

public class ArCondicionadoGellaKazaAdapter : IArCondicionado
{
    private readonly ArCondicionadoGellaKaza _ar;

    public ArCondicionadoGellaKazaAdapter(ArCondicionadoGellaKaza ar)
    {
        _ar = ar;
    }

    public void IniciarUso()
    {
        _ar.Ativar();
    }

    public void FinalizarUso()
    {
        _ar.Desativar();
    }

    public void AumentarTemperatura()
    {
        _ar.AumentarTemperatura();
    }

    public void DiminuirTemperatura()
    {
        _ar.DiminuirTemperatura();
    }

    public void DefinirTemperatura(int temperatura)
    {
        while (_ar.GetTemperatura() < temperatura)
            _ar.AumentarTemperatura();

        while (_ar.GetTemperatura() > temperatura)
            _ar.DiminuirTemperatura();
    }

    public int TemperaturaAtual => _ar.GetTemperatura();
}