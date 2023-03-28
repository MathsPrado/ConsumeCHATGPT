using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace ChatGPTConsume.Service.Interface
{
    public class ConsumeChatGPTService : IConsumeChatGPTService
    {
        private readonly HttpClient _httpClient;

        public ConsumeChatGPTService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("authorization", "Bearer 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"); //YOU KEY https://platform.openai.com/account/api-keyst;
        }

        public async Task<string> PostQuestionAPI(string value)
        {
            var content = new StringContent("{\"model\": \"text-davinci-001\", \"prompt\": \"" + value + "\",\"temperature\": 1,\"max_tokens\": 100}",
                Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("https://api.openai.com/v1/completions", content);

            string responseString = await response.Content.ReadAsStringAsync();

            var dyData = JsonConvert.DeserializeObject<dynamic>(responseString);

            string guess = dyData!.choices[0].text;

            return guess;
        }

        static string GuessCommand(string raw)
        {
            var lastIndex = raw.LastIndexOf('\n');
            string guess = raw.Substring(lastIndex + 1);
            return guess;
        }

    }
}
