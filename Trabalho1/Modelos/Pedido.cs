using Trabalho1.Enumeradores;
using Trabalho1.Modelos.Base;

namespace Trabalho1.Modelos;

public class Pedido : EntidadeBase
{
    public IList<Livro> Livros { get; private set; }
    public ModalidadeEntrega ModalidadeEntrega { get; private set; }
    public decimal ValorEntrega { get; private set; }

    public Pedido()
    {
        Livros = new List<Livro>();
    }

    public Pedido(
        Guid id,
        List<Livro> livros,
        ModalidadeEntrega modalidadeEntrega,
        decimal valorEntrega)
    {
        Id = id;
        Livros = livros;
        ModalidadeEntrega = modalidadeEntrega;
        ValorEntrega = valorEntrega;
    }
}
