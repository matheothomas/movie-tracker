using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Premier.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Core;

namespace Premier.Service {

  public class OmdbService {
    HttpClient _client;
    private readonly string _apiKey;

    public OmdbService(HttpClient client, IConfiguration configuration) {
      _client = client;
      _apiKey = configuration["APIKey"];
    }

    public async List<Film> SearchByTitle(string title) {
      HttpResponseMessage response = await client.GetAsync($"http://www.omdbapi.com/?i={title}&apikey={_apiKey}");
      // CALL JSON convert
    }

    public Film GetByImdbId(string imdbId) {
    }
  }
}
