using Trabalho1.Enumeradores;

namespace Trabalho1.Contratos;

public sealed record SalvarPedidoContrato(ModalidadeEntrega Modalidade, IList<SalvarLivroContrato> Livros);