using System.Net.Http.Headers;
using QuestionaryApp.Gateway.Models;

namespace QuestionaryApp.Gateway
{
    public class QuestionnairesGateway
    {
        private string _urlBase;
        private HttpClient _httpClient;
        private List<AnswerModel>? answers = new List<AnswerModel>();
        private List<QuestionModel>? questions = new List<QuestionModel>();
        public QuestionnairesGateway()
        {
            _httpClient = new HttpClient();
            _urlBase = "https://questionnaire-api.onrender.com/v1";

            _httpClient.BaseAddress = new Uri(_urlBase);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<(List<QuestionModel>?, List<AnswerModel>?)> GetRequestsData()
        {
            return (await GetQuestionsAsync(), await GetAnswersAsync());
        }

        private async Task<List<QuestionModel>?> GetQuestionsAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_urlBase}/questions");
            return await response.Content.ReadFromJsonAsync<List<QuestionModel>>();
        }

        private async Task<List<AnswerModel>?> GetAnswersAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_urlBase}/answers");
            return await response.Content.ReadFromJsonAsync<List<AnswerModel>>();
        }
    }
}