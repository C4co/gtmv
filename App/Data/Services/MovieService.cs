using System.Text.Json;

using Domain.Entities;

namespace Data.Services
{
    public class SearchResponseData
    {
        public int? MovieCount { get; set; } = null!;

        public int? Limit { get; set; } = null!;

        public int? PageNumber { get; set; } = null!;

        public List<Movie> Movies { get; set; } = null!;
    }

    public class SearchMovieResponse
    {
        public string? Status { get; set; }

        public string? StatusMessage { get; set; }

        public SearchResponseData? Data { get; set; } = null!;
    }

    public class MovieService
    {
        private readonly HttpClient _httpClient;

        public MovieService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://yts.mx/api/v2/");
        }

        public async Task<SearchMovieResponse> Search(string query)
        {
            var response = await _httpClient.GetAsync($"list_movies.json?query_term={query}&limit=50&sort_by=year");
            var content = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return JsonSerializer.Deserialize<SearchMovieResponse>(content, options)!;
        }
    }
}