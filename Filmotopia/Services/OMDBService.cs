namespace Filmotopia.Services;
using Filmotopia.Models;

public class OMDBService {
    private readonly HttpClient _httpClient;

    public OMDBService(HttpClient httpClient) {
        _httpClient = httpClient;
    }

    public async Task<List<Film>> GetFilm(string s)
    {
        var film = await _httpClient.GetFromJsonAsync<List<Film>>($"http://localhost:5041/api/OMDB/search?title={s}");
        return film;
    }
}
