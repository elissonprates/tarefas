namespace Domain.Base;

public interface IEntity
{
    int? Codigo { get; set; }

    (bool sucesso, ICollection<string> mensagens) Validar();
}

