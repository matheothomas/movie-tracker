namespace Filmotopia.Services;
using Filmotopia.Models;

public class FavoriteService {
    private readonly HttpClient _httpClient;

    public FavoriteService (HttpClient httpClient) {
        _httpClient = httpClient;
    }

    public async Task<List<Film>> GetFilms() {
        var films = await _httpClient.GetFromJsonAsync<List<Film>>("http://localhost:5041/api/Favorite/Film/0");
        return films;
    }

	public async Task<List<int>> GetFilmsById() {
		var filmsId = await _httpClient.GetFromJsonAsync<List<int>>("http://localhost:5041/api/Favorite/0");
		return filmsId;
	}

	public async Task RemoveFilm(int userId, int filmId) {
		await _httpClient.DeleteAsync($"http://localhost:5041/api/Favorite/remove?UserId={userId}&FilmId={filmId}");
	}

	public async Task AddFilm(int userId, int filmId) {
		await _httpClient.PostAsync($"http://localhost:5041/api/Favorite/add?UserId={userId}&FilmId={filmId}", null);
	}

}
