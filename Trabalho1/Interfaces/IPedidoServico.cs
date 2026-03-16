using Trabalho1.Contratos;
using Trabalho1.Modelos;

namespace Trabalho1.Interfaces;

public interface IPedidoServico
{
    Pedido GerarPedido(SalvarPedidoContrato pedidoContrato);
}
