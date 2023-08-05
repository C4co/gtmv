using Domain.Entities;

namespace View
{
    class Results
    {
        public static void ShowMovieResult(Movie movie)
        {
            Console.WriteLine();
            Console.WriteLine($"{Txt.White("----- MOVIE -----")}");
            Console.WriteLine();
            Console.WriteLine($"{Txt.Yellow(movie.Year.ToString())} - {Txt.Cyan(movie.Title)}");
            Console.WriteLine($"Genres: {Txt.Green(String.Join(", ", movie.Genres))}");
            Console.WriteLine($"Language: {Txt.Green(movie.Language)}");
            Console.WriteLine($"Cover: {Txt.Green(movie.LargeCoverImage!)}");
            Console.WriteLine($"Trailer: {Txt.Green("https://www.youtube.com/watch?v=" + movie.YtTrailerCode)}");
            Console.WriteLine();
        }

        public static void ShowTorrentResult(Torrent torrent)
        {
            Console.WriteLine($"{Txt.White("----- TORRENT -----")}");
            Console.WriteLine();
            Console.WriteLine($"{Txt.CyanBg($" [YTS] Torrent file {torrent.Quality} ")}: {Txt.Green(torrent.Url)}");
            Console.WriteLine();
            Console.WriteLine($"{Txt.CyanBg($" Magnet link {torrent.Quality} ")}: {Txt.Green(torrent.GetMagnetLink()!)}");
            Console.WriteLine();
        }

        public static void ShowSubtitleResult(Subtitle subtitle)
        {
            Console.WriteLine($"{Txt.White("----- SUBTITLE -----")}");
            Console.WriteLine();
            Console.WriteLine($"{Txt.CyanBg($" Subtitle {subtitle.Language} ")}: {Txt.Green(subtitle.DownloadLink!)}");
            Console.WriteLine();
        }
    }
}