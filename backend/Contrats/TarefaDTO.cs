using Domain.Enuns;

namespace Contracts;

public class TarefaDTO
{
    public int Codigo { get; set; }
    public string? Descricao { get; set; }
    public DateTime? Data { get; set; }
    public TarefaStatusEnum Status { get; set; }
}
