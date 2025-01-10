namespace Filmotopia.Services;
using Filmotopia.Models;

public class OMDBService {
    private readonly HttpClient _httpClient;

    public OMDBService(HttpClient httpClient) {
        _httpClient = httpClient;
    }

    public async Task<Film> GetFilm(string s)
    {
        var film = await _httpClient.GetFromJsonAsync<Film>("http://localhost:5041/api/OMDB/Search/{s}");
        return film;
    }
}
