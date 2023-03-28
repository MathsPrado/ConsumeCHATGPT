using ChatGPTConsume.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ChatGPTConsume.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumeChatGPT : ControllerBase
    {
        private readonly IConsumeChatGPTService _service;

        public ConsumeChatGPT(IConsumeChatGPTService service)
        {
            _service = service;
        }



        [HttpPost("responder-pergunta")]
        public async Task<ActionResult> Responder(string pergunta)
        {
            if (string.IsNullOrEmpty(pergunta))
                return BadRequest();

            var result = await _service.PostQuestionAPI(pergunta);

            if (string.IsNullOrEmpty(result))
            {
                return BadRequest();
            }
            return Ok(result);
        }

        static string GuessCommand(string raw)
        {
            var lastIndex = raw.LastIndexOf('\n');
            string guess = raw.Substring(lastIndex + 1);
            return guess;
        }
    }
}
