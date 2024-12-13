namespace Filmotopia.Services;
using Filmotopia.Models;

public class FilmService {
    private readonly HttpClient _httpClient;

    public FilmService (HttpClient httpClient) {
        _httpClient = httpClient;
    }

    public async Task<List<Film>> GetFilms() {
        var films = await _httpClient.GetFromJsonAsync<List<Film>>("http://localhost:5041/api/Film");
        return films;
    }

}
