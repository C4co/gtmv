using System.Drawing;

using Domain.Entities;

using Pastel;

namespace View
{
    class Formater
    {
        public static string FormatMovieOption(Movie movie)
        {
            return $"{Txt.Yellow(movie.Year.ToString())} | {Txt.Cyan(movie.Title)} - {movie.Id}";
        }

        public static string FormatTorrentOption(Torrent torrent, int index)
        {
            string title = $"[YTS] Torrent {torrent.Quality}";
            string seedPeers = $"Seeds: {torrent.Seeds} - Peers: {torrent.Peers}";

            return $"{index} | {Txt.Cyan(title)} | {torrent.Size} - {torrent.Type} | {Txt.Yellow(seedPeers)}";
        }

        public static string FormatSubtitleOption(Subtitle subtitle, int index)
        {
            var colorRating = Color.Black;
            var bakcgroundColorRating = Color.Yellow;

            if (int.Parse(subtitle.Rating!) > 0)
            {
                bakcgroundColorRating = Color.Green;
            }
            else if (int.Parse(subtitle.Rating!) < 0)
            {
                bakcgroundColorRating = Color.Red;
            }

            string rating = $" {subtitle.Rating} ".Pastel(colorRating).PastelBg(bakcgroundColorRating);

            return $"{rating} | {index} | {subtitle.Language}";
        }
    }
}