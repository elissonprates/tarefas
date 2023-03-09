using FluentValidation;

namespace Domain.Entities;

public class TarefaValidador : AbstractValidator<TarefaEntity>
{
    public TarefaValidador()
    {
        RuleFor(x => x.Descricao).NotEmpty().WithMessage("Por favor, informe a descrição da tarefa.");
    }
}