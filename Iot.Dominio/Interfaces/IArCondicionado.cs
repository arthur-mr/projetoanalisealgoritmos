namespace Iot.Dominio.Interfaces;

public interface IArCondicionado : IDispositivoIot
{
    void AumentarTemperatura();
    void DiminuirTemperatura();
    void DefinirTemperatura(int temperatura);
    int TemperaturaAtual { get; }
}
