using System.Drawing;
using Data.Repositories;
using Domain.Entities;
using Kurukuru;
using Pastel;
using Sharprompt;
using View;

class Runner
{
    static String FormatMovieOption(Movie movie)
    {
        return $"{Txt.Yellow(movie.year.ToString())} | {Txt.Cyan(movie.title)} - {movie.id}";
    }

    static String FormatTorrentOption(Torrent torrent, int index)
    {
        String title = $"[YTS] Torrent {torrent.quality}";
        String seedPeers = $"Seeds: {torrent.seeds} - Peers: {torrent.peers}";

        return $"{index} | {Txt.Cyan(title)} | {torrent.size} - {torrent.type} | {Txt.Yellow(seedPeers)}";
    }

    static String FormatSubtitleOption(Subtitle subtitle, int index)
    {
        var colorRating = Color.Black;
        var bakcgroundColorRating = Color.White;

        if (int.Parse(subtitle.rating!) > 0)
        {
            bakcgroundColorRating = Color.Green;
        }
        else if (int.Parse(subtitle.rating!) < 0)
        {
            bakcgroundColorRating = Color.Red;
        }

        String rating = $" ♥ {subtitle.rating} ".Pastel(colorRating).PastelBg(bakcgroundColorRating);

        return $"{rating} | {index} | {subtitle.language} ";
    }

    static void ShowResult(Movie movie, Torrent torrent, Subtitle subtitle)
    {
        Console.WriteLine();

        Console.WriteLine($"{Txt.White("▬▬▬▬▬ MOVIE ▬▬▬▬▬")}");

        Console.WriteLine();

        Console.WriteLine($"{Txt.Yellow(movie.year.ToString())} - {Txt.Cyan(movie.title)}");
        Console.WriteLine($"Genres: {Txt.Green(String.Join(", ", movie.genres))}");
        Console.WriteLine($"Language: {Txt.Green(movie.language)}");
        Console.WriteLine($"Cover: {Txt.Green(movie.large_cover_image!)}");
        Console.WriteLine($"Trailer: {Txt.Green("https://www.youtube.com/watch?v=" + movie.yt_trailer_code)}");

        Console.WriteLine();

        Console.WriteLine($"{Txt.White("▬▬▬▬▬ TORRENT ▬▬▬▬▬")}");

        Console.WriteLine();

        Console.WriteLine($"{Txt.CyanBg($" [YTS] Torrent file {torrent.quality} ")}: {Txt.Green(torrent.url)}");

        Console.WriteLine();

        Console.WriteLine($"{Txt.CyanBg($" Magnet link {torrent.quality} ")}: {Txt.Green(torrent.getMagnetLink()!)}");

        Console.WriteLine();

        Console.WriteLine($"{Txt.White("▬▬▬▬▬ SUBTITLE ▬▬▬▬▬")}");

        Console.WriteLine();

        Console.WriteLine($"{Txt.CyanBg($" Subtitle {subtitle.language} ")}: {Txt.Green(subtitle.downloadLink!)}");

        Console.WriteLine();
    }

    static public async Task Run()
    {
        HttpClient httpClient = new HttpClient();

        var movieRepository = new MovieRepository(httpClient);
        var subtitleRepository = new SubtitleRepository(httpClient);

        SearchMovieResponse? movies = null;

        List<Subtitle> subtitles = new List<Subtitle>();

        var query = Prompt.Input<string>("Search movie");

        await Spinner.StartAsync("Searching...", async (spinner) =>
        {
            movies = await movieRepository.Search(query);

            if (query.Length == 0)
            {
                spinner.Text = "Lastests releases";
            }
            else
            {
                spinner.Text = $"Results for {query}";
            }

            if (movies?.data?.movie_count == 0)
            {
                spinner.Fail("No results found");
                Environment.Exit(0);
            }
        });

        var movieSelected = Prompt.Select($"Select movie: {movies?.data?.movie_count} results", movies?.data?.movies.Select(FormatMovieOption).ToList());
        var movieId = movieSelected.Split(" - ")[1];
        var movieFiltered = movies?.data?.movies.Where(movie => movie.id.ToString() == movieId).ToList().First();

        var torrentSelected = Prompt.Select("Select torrent", movieFiltered?.torrents.Select(FormatTorrentOption).ToList());
        var torrentIndex = torrentSelected.Split(" | ")[0];
        var torrentFiltered = movieFiltered?.torrents.Where((torrent, index) => index.ToString() == torrentIndex).ToList().First();

        await Spinner.StartAsync("Searching subtitle...", async (spinner) =>
        {
            subtitles = await subtitleRepository.getSubtitle(movieFiltered?.imdb_code!);

            if (subtitles.Count == 0)
            {
                spinner.Fail("No subtitles found");
            }
        });

        var subtitleSelected = Prompt.Select("Select subtitle", subtitles.Select(FormatSubtitleOption).ToList());
        var subtitleIndex = subtitleSelected.Split(" | ")[1];
        var subtitleFiltered = subtitles.Where((subtitle, index) => index.ToString() == subtitleIndex).ToList().First();

        ShowResult(movieFiltered!, torrentFiltered!, subtitleFiltered!);
    }
}

class Program
{
    static async Task Main(string[] args)
    {
        while (true)
        {
            await Runner.Run();

            var option = Prompt.Select("Want to search for another movie?", new List<string> { "Yes", "No" });

            if (option == "No")
            {
                Environment.Exit(0);
            }
        }
    }
}
