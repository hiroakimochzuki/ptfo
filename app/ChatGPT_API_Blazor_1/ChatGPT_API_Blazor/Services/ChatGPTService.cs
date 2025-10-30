using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace ChatGPT_API_Blazor.Services
{
    public class ChatGPTService
    {
        private HttpClient _client = new HttpClient();
        //★(a)APIキーを環境変数で指定 
        static readonly string _apiKey = Environment.GetEnvironmentVariable("GPT_APIKEY");
        static readonly string _apiEndpoint = Environment.GetEnvironmentVariable("GPT_ENDPOINT");

        private List<Message> _chatHistory = new List<Message>();

        public async Task<string> AskGPT4(string message)
        {
            // 履歴にユーザー発言を追加
            _chatHistory.Add(new Message { role = "user", content = message });

            var requestData = new GPTRequest
            {
                model = "gpt-4o",
                messages = _chatHistory.ToArray() // 履歴をすべて送信
            };

            var requestContent = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(requestContent, Encoding.UTF8, "application/json");

            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);
            var response = await _client.PostAsync(_apiEndpoint, content);

            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<GPTResponse>(responseBody);

            var reply = responseObject.choices[0].message.content;

            // 履歴にアシスタント発言を追加
            _chatHistory.Add(new Message { role = "assistant", content = reply });

            return reply;
        }

        //★(d)リクエストのデータ定義
        record Message
        {
            public string role { get; init; } = null!;
            public string content { get; init; } = null!;
        }

        record GPTRequest
        {
            public string model { get; init; } = null!;
            public Message[] messages { get; init; } = null!;
        }

        record GPTResponse
        {
            public Choice[] choices { get; init; } = null!;
        }

        record Choice
        {
            public InnerMessage message { get; init; } = null!;
        }

        record InnerMessage
        {
            public string content { get; init; } = null!;
        }
    }
}