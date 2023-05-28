using System.Drawing;

using Domain.Entities;

using Pastel;

namespace View
{
    class Formater
    {
        public static String FormatMovieOption(Movie movie)
        {
            return $"{Txt.Yellow(movie.year.ToString())} | {Txt.Cyan(movie.title)} - {movie.id}";
        }

        public static String FormatTorrentOption(Torrent torrent, int index)
        {
            String title = $"[YTS] Torrent {torrent.quality}";
            String seedPeers = $"Seeds: {torrent.seeds} - Peers: {torrent.peers}";

            return $"{index} | {Txt.Cyan(title)} | {torrent.size} - {torrent.type} | {Txt.Yellow(seedPeers)}";
        }

        public static String FormatSubtitleOption(Subtitle subtitle, int index)
        {
            var colorRating = Color.Black;
            var bakcgroundColorRating = Color.Yellow;

            if (int.Parse(subtitle.rating!) > 0)
            {
                bakcgroundColorRating = Color.Green;
            }
            else if (int.Parse(subtitle.rating!) < 0)
            {
                bakcgroundColorRating = Color.Red;
            }

            String rating = $" {subtitle.rating} ".Pastel(colorRating).PastelBg(bakcgroundColorRating);

            return $"{rating} | {index} | {subtitle.language}";
        }
    }
}