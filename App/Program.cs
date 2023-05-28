using Data.Repositories;

using Domain.Entities;

using Kurukuru;

using Sharprompt;

using View;

class Runner
{
    static public async Task Run()
    {
        HttpClient httpClient = new HttpClient();
        var movieRepository = new MovieRepository(httpClient);
        var subtitleRepository = new SubtitleRepository(httpClient);
        SearchMovieResponse? response = null;
        List<Subtitle> subtitles = new List<Subtitle>();
        bool foundMovie = false;

        while (!foundMovie)
        {
            var query = Prompt.Input<string>("Search movie", validators: new[] {
                Validators.Required("Movie is required")
            });

            await Spinner.StartAsync("Searching...", async (spinner) =>
            {
                response = await movieRepository.Search(query);

                if (response?.data?.movie_count == 0)
                {
                    spinner.Fail("No results found, try again");
                }
                else
                {
                    foundMovie = true;
                    spinner.Text = $"Results for {query}";
                }
            });
        }

        var movieSelected = Prompt.Select($"Select movie: {response?.data?.movie_count} results", response?.data?.movies.Select(Formater.FormatMovieOption).ToList());
        var movieId = movieSelected.Split(" - ")[1];
        var movieFiltered = response?.data?.movies.Where(movie => movie.id.ToString() == movieId).ToList().First();

        var torrentSelected = Prompt.Select("Select torrent", movieFiltered?.torrents.Select(Formater.FormatTorrentOption).ToList());
        var torrentIndex = torrentSelected.Split(" | ")[0];
        var torrentFiltered = movieFiltered?.torrents.Where((torrent, index) => index.ToString() == torrentIndex).ToList().First();

        await Spinner.StartAsync("Searching subtitle...", async (spinner) =>
        {
            try
            {
                subtitles = await subtitleRepository.getSubtitle(movieFiltered?.imdb_code!);
            }
            catch (System.Net.Http.HttpRequestException)
            {
                spinner.Fail("No subtitles found");
            }
        });

        if (subtitles.Count != 0)
        {
            var subtitleSelected = Prompt.Select("Select subtitle", subtitles.Select(Formater.FormatSubtitleOption).ToList());
            var subtitleIndex = subtitleSelected.Split(" | ")[1];
            var subtitleFiltered = subtitles.Where((subtitle, index) => index.ToString() == subtitleIndex).ToList().First();
            Results.ShowMovieResult(movieFiltered!);
            Results.ShowTorrentResult(torrentFiltered!);
            Results.ShowSubtitleResult(subtitleFiltered!);
        }
        else
        {
            Results.ShowMovieResult(movieFiltered!);
            Results.ShowTorrentResult(torrentFiltered!);
        }
    }
}

class Program
{
    static async Task Main(string[] args)
    {
        while (true)
        {
            await Runner.Run();
        }
    }
}
