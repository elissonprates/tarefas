using FluentValidation;

namespace Domain.Entities;

public class TarefaValidador : AbstractValidator<TarefaEntity>
{
    public TarefaValidador()
    {
        RuleFor(x => x.Descricao)
            .NotEmpty().WithMessage("Por favor, informe a descrição da tarefa.")
            .MinimumLength(10).WithMessage("A descrição da tarefa deve ter no mínimo 10 caracteres.")
            .MaximumLength(50).WithMessage("A descrição da tarefa deve ter no máximo 50 caracteres.");  
    }
}