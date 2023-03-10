using Domain.Base;
using Domain.Enuns;

namespace Domain.Entities;

public class TarefaEntity: IEntity
{
    public int? Codigo { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }

    public (bool sucesso, ICollection<string> mensagens) Validar()
    {
        var validator = new TarefaValidador();

        var resultadoDaValidacao = validator.Validate(this);

        return (resultadoDaValidacao.IsValid, resultadoDaValidacao.Errors.Select(x => x.ErrorMessage).ToList());
    }
}
