using Contrats;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("tarefas")]
public class TarefaController : ControllerBase
{
    private readonly ITarefaService _service;
    private readonly ILogger<TarefaController> _logger;

    public TarefaController(ITarefaService service, ILogger<TarefaController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTarefas()
    {
        _service.
        return null;
    }


    [HttpGet]
    [Route("{codigo}")]
    public async Task<IActionResult> ObterTarefaPorCodigo([FromRoute] int codigo)
    {
        return null;
    }

    [HttpPost]
    public async Task<IActionResult> SalvarTarefa([FromBody] TarefaDTO dto)
    {
        return null;
    }
}