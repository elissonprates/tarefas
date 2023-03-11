using Contracts;
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
        var retorno = await _service.ObterTarefas();
        return Ok(retorno);
    }

    [HttpGet]
    [Route("{codigo}")]
    public async Task<IActionResult> ObterTarefaPorCodigo([FromRoute] int codigo)
    {
        var retorno = await _service.ObterTarefaPorCodigo(codigo);
        return Ok(retorno);
    }

    [HttpPost]
    public async Task<IActionResult> Incluir([FromBody] TarefaDTO dto)
    {
        var retorno = await _service.Incluir(dto);
        return Ok(retorno);
    }

    [HttpPut]
    public async Task<IActionResult> Alterar([FromBody] TarefaDTO dto)
    {
        var retorno = await _service.Alterar(dto);
        return Ok(retorno);
    }

    [HttpDelete]
    public async Task<IActionResult> Excluir([FromQuery(Name ="codigo")] int[] codigos)
    {
        RetornoApi retorno = null;

        foreach (var codigo in codigos)
        {
            retorno = await _service.Excluir(codigo);

            if (retorno.sucesso == false)
            {
                return Ok(retorno);
            }
        }

        return Ok(retorno);
    }
}