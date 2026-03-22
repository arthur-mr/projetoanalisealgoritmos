using B3.Dominio.Interfaces;
using B3.Dominio.Mensagens;
using B3.Dominio.Servicos.Modulos;

namespace B3.Consumidores.Consumidores;

internal sealed class PrecoAcaoAlteradoConsumidor : ConsumidorBase<PrecoAcaoAlterado>
{
    private readonly IAcaoServico servico;

    public PrecoAcaoAlteradoConsumidor(IAcaoServico servico, ConfiguracaoMensageria configuracaoMensageria)
        : base(configuracaoMensageria)
    {
        this.servico = servico;
    }

    public override async Task ConsumirMensagem(PrecoAcaoAlterado mensagem, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Consumindo mensagem: {mensagem}");
        await servico.NotificarAlteracaoParaInvestidoresAsync(mensagem.AcaoId, cancellationToken);
        Console.WriteLine($"Consumida mensagem: {mensagem}");
    }
}
