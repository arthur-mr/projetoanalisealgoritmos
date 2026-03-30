namespace Iot.Dominio.Interfaces;

public interface ICasaInteligente
{
    void AbrirPersianas();
    void AumentarTemperaturaTodos();
    void DefinirTemperaturaTodos(int temperatura);
    void DesligarAres();
    void DesligarLampadas();
    void DiminuirTemperaturaTodos();
    void FecharPersianas();
    void LigarAres();
    void LigarLampadas();
    void ModoSono();
    void ModoTrabalho();
}