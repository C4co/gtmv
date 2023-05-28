using Domain.Entities;

namespace View
{
    class Results
    {
        public static void ShowMovieResult(Movie movie)
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
        }

        public static void ShowTorrentResult(Torrent torrent)
        {
            Console.WriteLine($"{Txt.White("▬▬▬▬▬ TORRENT ▬▬▬▬▬")}");
            Console.WriteLine();
            Console.WriteLine($"{Txt.CyanBg($" [YTS] Torrent file {torrent.quality} ")}: {Txt.Green(torrent.url)}");
            Console.WriteLine();
            Console.WriteLine($"{Txt.CyanBg($" Magnet link {torrent.quality} ")}: {Txt.Green(torrent.getMagnetLink()!)}");
            Console.WriteLine();
        }

        public static void ShowSubtitleResult(Subtitle subtitle)
        {
            Console.WriteLine($"{Txt.White("▬▬▬▬▬ SUBTITLE ▬▬▬▬▬")}");
            Console.WriteLine();
            Console.WriteLine($"{Txt.CyanBg($" Subtitle {subtitle.language} ")}: {Txt.Green(subtitle.downloadLink!)}");
            Console.WriteLine();
        }
    }
}