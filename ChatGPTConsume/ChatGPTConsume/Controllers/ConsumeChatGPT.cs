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
        private readonly HttpClient _httpClient;

        public ConsumeChatGPT(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("authorization", "Bearer YOUR KEY"); //YOU KEY https://platform.openai.com/account/api-keys

        }


        [HttpPost("responder-pergunta")]
        public async Task<ActionResult> Responder(string pergunta)
        {
            var content = new StringContent("{\"model\": \"text-davinci-001\", \"prompt\": \"" + pergunta + "\",\"temperature\": 1,\"max_tokens\": 100}",
                Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("https://api.openai.com/v1/completions", content);

            string responseString = await response.Content.ReadAsStringAsync();

            var dyData = JsonConvert.DeserializeObject<dynamic>(responseString);

            string guess = dyData!.choices[0].text;

            if (string.IsNullOrEmpty(guess))
            {
                return BadRequest();
            }
            return Ok(guess);
        }

        static string GuessCommand(string raw)
        {
            var lastIndex = raw.LastIndexOf('\n');
            string guess = raw.Substring(lastIndex + 1);
            return guess;
        }
    }
}
