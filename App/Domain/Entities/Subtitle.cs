namespace Domain.Entities
{
    public class Subtitle
    {
        public string? language { get; set; }
        public string? downloadLink { get; set; }
        public string? rating { get; set; }

        public override String ToString()
        {
            return $"{rating} - {language} - {downloadLink}";
        }
    }
}