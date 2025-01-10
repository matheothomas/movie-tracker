namespace Filmotopia.Services;
using Filmotopia.Models;

public class FavoriteService {
    private readonly HttpClient _httpClient;

    public FavoriteService (HttpClient httpClient) {
        _httpClient = httpClient;
    }

    public async Task<List<Film>> GetFilms() {
        var films = await _httpClient.GetFromJsonAsync<List<Film>>("http://localhost:5041/api/Favorite/0");
        return films;
    }

}
