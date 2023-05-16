using System.Text.Json;
using Domain.Entities;

namespace Data.Repositories;

public class SearchResponseData
{
    public int? movie_count { get; set; } = null!;

    public int? limit { get; set; } = null!;

    public int? page_number { get; set; } = null!;

    public List<Movie> movies { get; set; } = null!;
}

public class SearchMovieResponse
{
    public string? status { get; set; }

    public string? status_message { get; set; }

    public SearchResponseData? data { get; set; } = null!;
}

public class MovieRepository
{
    private static readonly HttpClient _httpClient = new HttpClient();

    public MovieRepository()
    {
        _httpClient.BaseAddress = new Uri("https://yts.mx/api/v2/");
    }

    public async Task<SearchMovieResponse> Search(string query)
    {
        var response = await _httpClient.GetAsync($"list_movies.json?query_term={query}&limit=50&sort_by=year");
        var content = await response.Content.ReadAsStringAsync();

        var res = JsonSerializer.Deserialize<SearchMovieResponse>(content);

        return res!;
    }
}
