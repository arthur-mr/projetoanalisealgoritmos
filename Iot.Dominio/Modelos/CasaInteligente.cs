using Iot.Dominio.Interfaces;

namespace Iot.Dominio.Modelos;

internal sealed class CasaInteligente : ICasaInteligente
{
    private readonly IEnumerable<ILampada> _lampadas;
    private readonly IEnumerable<IPersiana> _persianas;
    private readonly IEnumerable<IArCondicionado> _ares;

    public CasaInteligente(
        IEnumerable<ILampada> lampadas,
        IEnumerable<IPersiana> persianas,
        IEnumerable<IArCondicionado> ares)
    {
        _lampadas = lampadas;
        _persianas = persianas;
        _ares = ares;
    }

    public void LigarLampadas()
    {
        foreach (var lampada in _lampadas)
            lampada.IniciarUso();
    }

    public void DesligarLampadas()
    {
        foreach (var lampada in _lampadas)
            lampada.FinalizarUso();
    }

    public void AbrirPersianas()
    {
        foreach (var persiana in _persianas)
            persiana.IniciarUso();
    }

    public void FecharPersianas()
    {
        foreach (var persiana in _persianas)
            persiana.FinalizarUso();
    }

    public void LigarAres()
    {
        foreach (var ar in _ares)
            ar.IniciarUso();
    }

    public void DesligarAres()
    {
        foreach (var ar in _ares)
            ar.FinalizarUso();
    }

    public void AumentarTemperaturaTodos()
    {
        foreach (var ar in _ares)
            ar.AumentarTemperatura();
    }

    public void DiminuirTemperaturaTodos()
    {
        foreach (var ar in _ares)
            ar.DiminuirTemperatura();
    }

    public void DefinirTemperaturaTodos(int temperatura)
    {
        foreach (var ar in _ares)
            ar.DefinirTemperatura(temperatura);
    }

    public void ModoSono()
    {
        DesligarLampadas();
        DesligarAres();
        FecharPersianas();
    }

    public void ModoTrabalho()
    {
        LigarLampadas();
        LigarAres();
        DefinirTemperaturaTodos(25);
        AbrirPersianas();
    }
}
