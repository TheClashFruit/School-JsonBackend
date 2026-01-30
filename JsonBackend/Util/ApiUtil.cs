using System.Net.Http;
using System.Net.Http.Json;
using JsonBackend.Model;

namespace JsonBackend.Util;

public class ApiUtil {
    private static HttpClient _client = new();

    public static async Task<List<Person>> GetPersons() {
        return await _client.GetFromJsonAsync<List<Person>>("https://retoolapi.dev/prTuO5/ccp") ?? throw new Exception("Failed to fetch persons!");
    }

    public static async void DeletePerson(int id) {
        var response = await _client.DeleteAsync($"https://retoolapi.dev/prTuO5/ccp/{id}");
        response.EnsureSuccessStatusCode();
    }

    public static async void UpdatePerson(Person person) {
        var response = await _client.PutAsJsonAsync($"https://retoolapi.dev/prTuO5/ccp/{person.Id}", person);
        response.EnsureSuccessStatusCode();
    }
    
    public static async Task<Person?> CreatePerson(Person person) {
        var response = await _client.PostAsJsonAsync("https://retoolapi.dev/prTuO5/ccp", person);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Person>();
    }

}