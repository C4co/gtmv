using Data.Services;

using Domain.Entities;

using Kurukuru;

using Sharprompt;

using View;

class Runner
{
    static public async Task Run()
    {
        HttpClient httpClient = new();
        var movieService = new MovieService(httpClient);
        var subtitleService = new SubtitleService(httpClient);
        SearchMovieResponse? response = null;
        List<Subtitle> subtitles = new();
        bool foundMovie = false;

        while (!foundMovie)
        {
            var query = Prompt.Input<string>("Search movie", validators: new[] {
                Validators.Required("Movie is required")
            });

            await Spinner.StartAsync("Searching...", async (spinner) =>
            {
                response = await movieService.Search(query);

                if (response?.Data?.MovieCount == 0)
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

        var movieSelected = Prompt.Select($"Select movie: {response?.Data?.MovieCount} results", response?.Data?.Movies.Select(Formater.FormatMovieOption).ToList());
        var movieId = movieSelected.Split(" - ")[1];
        var movieFiltered = response?.Data?.Movies.Where(movie => movie.Id.ToString() == movieId).ToList().First();

        var torrentSelected = Prompt.Select("Select torrent", movieFiltered?.Torrents.Select(Formater.FormatTorrentOption).ToList());
        var torrentIndex = torrentSelected.Split(" | ")[0];
        var torrentFiltered = movieFiltered?.Torrents.Where((torrent, index) => index.ToString() == torrentIndex).ToList().First();

        await Spinner.StartAsync("Searching subtitle...", async (spinner) =>
        {
            try
            {
                subtitles = await subtitleService.GetSubtitle(movieFiltered?.ImdbCode!);
            }
            catch (HttpRequestException)
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
    static async Task Main()
    {
        while (true)
        {
            await Runner.Run();
        }
    }
}
