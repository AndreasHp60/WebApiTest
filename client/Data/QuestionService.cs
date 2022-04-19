using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Model;

namespace TodoListBlazor.Data;

public class QuestionService
{
    private readonly HttpClient http;
    private readonly IConfiguration configuration;
    private readonly string baseAPI = "";

    public QuestionService(HttpClient http, IConfiguration configuration) {
        this.http = http;
        this.configuration = configuration;
        baseAPI = configuration["base_api"];
    }

    public async Task<Questions[]> GetQuestionsData()
    {
        string url = $"{baseAPI}questions";
        return await http.GetFromJsonAsync<Questions[]>(url);
    }

    public async Task<Category[]> GetCategoriesData()
    {
        string url = $"{baseAPI}categories";
        return await http.GetFromJsonAsync<Category[]>(url);
    }

    public async Task<Questions> GetQuestionData(int id)
    {
        string url = $"{baseAPI}questions/{id}";
        return await http.GetFromJsonAsync<Questions>(url);
    }

    public async void PostQuestionData(Questions question, string[] Cate)
    {
        string url = $"{baseAPI}question/";
        QuestionData newQuestion = new(question.Date, question.Headline, question.Question, question.User.Name, Cate);
        await http.PostAsJsonAsync(url, newQuestion);
    }

    public async void PostAnswerData(Answers answer, long id)
    {
        string url = $"{baseAPI}question/{id}/answers";
        AnswerData newAnswer = new(answer.Date, answer.Answer, answer.User.Name);
        await http.PostAsJsonAsync(url, newAnswer);
    }

    record QuestionData(DateTime date, string headline, string question, string name, string[] category);
    record AnswerData(DateTime date, string answer, string name);


}
