using AlgoritmosDotNet;
using FluentAssertions;

namespace Iot.Testes;

public class CasaInteligenteTestes : TesteBase
{
    [Fact]
    public void Deve_definir_temperatura_de_todos_os_ares()
    {
        // Act
        Casa.DefinirTemperaturaTodos(23);

        // Assert
        ObterServico<ArCondicionadoVentoBaumn>().GetTemperatura().Should().Be(23);
        ObterServico<ArCondicionadoGellaKaza>().GetTemperatura().Should().Be(23);
    }

    [Fact]
    public void Deve_aumentar_temperatura_de_todos_os_ares()
    {
        // Arrange
        Casa.DefinirTemperaturaTodos(20);

        // Act
        Casa.AumentarTemperaturaTodos();

        // Assert
        ObterServico<ArCondicionadoVentoBaumn>().GetTemperatura().Should().Be(21);
        ObterServico<ArCondicionadoGellaKaza>().GetTemperatura().Should().Be(21);
    }

    [Fact]
    public void Deve_diminuir_temperatura_de_todos_os_ares()
    {
        // Arrange
        Casa.DefinirTemperaturaTodos(20);

        // Act
        Casa.DiminuirTemperaturaTodos();

        // Assert
        ObterServico<ArCondicionadoVentoBaumn>().GetTemperatura().Should().Be(19);
        ObterServico<ArCondicionadoGellaKaza>().GetTemperatura().Should().Be(19);
    }

    [Fact]
    public void Deve_ligar_todos_os_ares()
    {
        // Act
        Casa.LigarAres();

        // Assert
        ObterServico<ArCondicionadoVentoBaumn>().EstaLigado().Should().BeTrue();
        ObterServico<ArCondicionadoGellaKaza>().EstaLigado().Should().BeTrue();
    }

    [Fact]
    public void Deve_desligar_todos_os_ares()
    {
        // Arrange
        Casa.LigarAres();

        // Act
        Casa.DesligarAres();

        // Assert
        ObterServico<ArCondicionadoVentoBaumn>().EstaLigado().Should().BeFalse();
        ObterServico<ArCondicionadoGellaKaza>().EstaLigado().Should().BeFalse();
    }

    [Fact]
    public void Modo_trabalho_deve_ligar_ares_e_definir_temperatura_25()
    {
        // Act
        Casa.ModoTrabalho();

        // Assert
        ObterServico<ArCondicionadoVentoBaumn>().GetTemperatura().Should().Be(25);
        ObterServico<ArCondicionadoGellaKaza>().GetTemperatura().Should().Be(25);
        ObterServico<ArCondicionadoVentoBaumn>().EstaLigado().Should().BeTrue();
        ObterServico<ArCondicionadoGellaKaza>().EstaLigado().Should().BeTrue();
    }

    [Fact]
    public void Modo_sono_deve_desligar_todos_os_ares()
    {
        // Arrange
        Casa.LigarAres();
        Casa.DefinirTemperaturaTodos(22);

        // Act
        Casa.ModoSono();

        // Assert
        ObterServico<ArCondicionadoVentoBaumn>().EstaLigado().Should().BeFalse();
        ObterServico<ArCondicionadoGellaKaza>().EstaLigado().Should().BeFalse();
    }
}