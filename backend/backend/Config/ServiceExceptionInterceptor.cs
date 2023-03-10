using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Config;

public class ServiceExceptionInterceptor : IExceptionFilter
{
    private readonly ILogger _logger;
    public ServiceExceptionInterceptor(ILogger<ServiceExceptionInterceptor> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, $"Erro na api: {context.HttpContext.Request.Path.ToString()}");

        var retorno = new RetornoApi("Erro no processamento", false);
        context.Result = new BadRequestObjectResult(retorno);
    }
}