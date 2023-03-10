using Domain.Enuns;

namespace Contracts;

public class TarefaDTO
{
    public int? codigo { get; set; }
    public string? descricao { get; set; }
    public DateTime? data { get; set; }
    public TarefaStatusEnum status { get; set; }
}
