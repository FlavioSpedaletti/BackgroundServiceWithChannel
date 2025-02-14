using Microsoft.AspNetCore.Mvc;

namespace BackgroundServiceWithChannel.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController(ILogger<EmailController> logger) : ControllerBase
    {
        [HttpGet("enviar-email")]
        public IActionResult EnviarEmail([FromServices] EmailQueueService queue)
        {
            queue.Enqueue(() => EnviarEmailAsync("teste@exemplo.com"));
            return Ok("E-mail será enviado em segundo plano!");
        }

        private async Task EnviarEmailAsync(string email)
        {
            await Task.Delay(5000);
            await Task.Run(() => logger.LogInformation("E-mail enviado para {email}", email));
        }
    }
}
