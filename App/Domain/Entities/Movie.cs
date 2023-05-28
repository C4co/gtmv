namespace Domain.Entities
{
    public class Movie
    {
        public int id { get; set; }

        public string title { get; set; } = null!;

        public string language { get; set; } = null!;

        public int year { get; set; }

        public List<string> genres { get; set; } = null!;

        public string yt_trailer_code { get; set; } = null!;

        public string? large_cover_image { get; set; }

        public List<Torrent> torrents { get; set; } = null!;

        public string imdb_code { get; set; } = null!;
    }
}