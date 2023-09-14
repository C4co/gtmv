using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Language { get; set; } = null!;

        public int Year { get; set; }

        public List<string> Genres { get; set; } = null!;

        [JsonPropertyName("yt_trailer_code")]
        public string YtTrailerCode { get; set; } = null!;

        [JsonPropertyName("large_cover_image")]
        public string? LargeCoverImage { get; set; }

        public List<Torrent> Torrents { get; set; } = null!;

        [JsonPropertyName("imdb_code")]
        public string ImdbCode { get; set; } = null!;

        public override string ToString()
        {
            return $@"Id: {Id}
                    Title: {Title}
                    Language: {Language}
                    Year: {Year}
                    Genres: {string.Join(", ", Genres)}
                    YtTrailerCode: {YtTrailerCode}
                    LargeCoverImage: {LargeCoverImage}
                    Torrents: {string.Join(", ", Torrents)}
                    ImdbCode: {ImdbCode}";
        }
    }

}


