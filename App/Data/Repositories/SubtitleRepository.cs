namespace Data.Repositories;

using Domain.Entities;

using HtmlAgilityPack;

public class SubtitleRepository
{
    private HttpClient _client;

    public SubtitleRepository(HttpClient httpClient)
    {
        _client = httpClient;
    }

    public async Task<List<Subtitle>> getSubtitle(String imdbCode)
    {
        List<Subtitle> subtitles = new List<Subtitle>();

        var response = await _client.GetAsync($"https://yts-subs.com/movie-imdb/{imdbCode}");
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();

        HtmlDocument htmlDoc = new HtmlDocument();

        htmlDoc.LoadHtml(responseBody);

        var highRating = htmlDoc.DocumentNode.SelectNodes("//tr[@class='high-rating']");

        foreach (var item in highRating)
        {
            var link = item.SelectSingleNode(".//a[@class='subtitle-download']");

            var lang = item.SelectSingleNode(".//span[@class='sub-lang']");

            var formatedLink = $"https://yifysubtitles.org{link.Attributes["href"].Value.Replace("subtitles", "subtitle")}.zip";

            var label = item.SelectSingleNode(".//td[@class='rating-cell']");

            subtitles.Add(
                new Subtitle
                {
                    language = lang.InnerText,
                    downloadLink = formatedLink,
                    rating = label.InnerText.Trim()
                }
            );
        }

        return subtitles;
    }
}
