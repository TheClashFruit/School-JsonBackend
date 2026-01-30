using System.Net.Http;
using System.Net.Http.Json;
using JsonBackend.Model;

namespace JsonBackend.Util;

public class ApiUtil {
    private static HttpClient _client = new() {
        BaseAddress = new Uri("https://retoolapi.dev/prTuO5/ccp")
    };

    public static async Task<List<Person>> GetPersons() {
        return await _client.GetFromJsonAsync<List<Person>>("") ?? throw new Exception("Failed to fetch persons!");
    }

    public static async void DeletePerson(int id) {
        var response = await _client.DeleteAsync($"/{id}");
        response.EnsureSuccessStatusCode();
    }
    
    public static async Task<Person?> CreatePerson(Person person) {
        var response = await _client.PostAsJsonAsync("", person);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Person>();
    }

}