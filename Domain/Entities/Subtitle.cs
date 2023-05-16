namespace Domain.Entities;

class Subtitle
{
    public String? language { get; set; }
    public String? downloadLink { get; set; }
    public String? rating { get; set; }

    public Subtitle(
        String? language,
        String? downloadLink,
        String? rating
    )
    {
        this.language = language;
        this.downloadLink = downloadLink;
        this.rating = rating;
    }

    public override String ToString()
    {
        return $"{rating} - {language} - {downloadLink}";
    }
}
