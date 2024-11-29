using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Premier.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Core;

namespace Premier.Services {

	public class OMDBService {
		HttpClient _client;
		private readonly string _apiKey;

		public OMDBService(HttpClient client, IConfiguration configuration) {
			_client = client;
			_apiKey = configuration["APIKey"];
		}

		public async Task<List<Film>> SearchByTitle(string title) {
			var searchResults = await _client.GetFromJsonAsync<OMDBSearchResponse>($"http://www.omdbapi.com/?s={title}&apikey={_apiKey}");

			var res = searchResults.Search;

			var films = new List<Film>();

			foreach (var omdbFilm in searchResults.Search) {
				var film = new Film
				{
					Id = films.Count + 1, 
					Title = omdbFilm.Title,
					Poster = omdbFilm.Poster,
					Imdb = omdbFilm.imdbID,
					Date = omdbFilm.Year
				};

				films.Add(film);
			}
			return films;
		}

		public async void GetByImdbId(string imdbId) {
		}
	}
}
