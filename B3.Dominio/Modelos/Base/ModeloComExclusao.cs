namespace B3.Dominio.Modelos.Base;

public class ModeloComExclusao : ModeloBase
{
    public bool Ativo { get; private set; }
    public DateTime DataAtivacao { get; private set; }
    public DateTime? DataDesativacao { get; private set; }

    protected ModeloComExclusao()
    { }
}