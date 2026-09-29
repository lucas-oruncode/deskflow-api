using Deskflow.API.DTOs.Error;

namespace Deskflow.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ArgumentException ex)
            {
                
                _logger.LogWarning($"Ocorreu um erro: {ex.Message}");
                context.Response.StatusCode = 400;
                var response = new ErrorDto("Argumento inválido.");
                await context.Response.WriteAsJsonAsync(response);
            }
            catch (KeyNotFoundException ex)
            {
                
                _logger.LogWarning($"Ocorreu um erro: {ex.Message}");
                context.Response.StatusCode = 404;
                var response = new ErrorDto("ID inválido.");
                await context.Response.WriteAsJsonAsync(response);
            }
            catch (InvalidOperationException ex)
            {
                
                _logger.LogWarning($"Ocorreu um erro: {ex.Message}");
                context.Response.StatusCode = 409;
                var response = new ErrorDto("Operação inválida.");
                await context.Response.WriteAsJsonAsync(response);
            }

            catch (Exception ex)
            {
                
                _logger.LogError($"Ocorreu um erro: {ex.Message}");
                context.Response.StatusCode = 500;
                var response = new ErrorDto("Ocorreu um erro interno no servidor.");
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}