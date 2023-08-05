namespace Domain.Entities
{
    public class Subtitle
    {
        public string? Language { get; set; }
        public string? DownloadLink { get; set; }
        public string? Rating { get; set; }

        public override string ToString()
        {
            return $"{Rating} - {Language} - {DownloadLink}";
        }
    }
}