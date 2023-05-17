using Domain.Entities;
using HtmlAgilityPack;
namespace Data.Repositories;

class SubtitleRepository
{
    private HttpClient _client;

    //contructor
    public SubtitleRepository(HttpClient client)
    {
        _client = client;
    }

    public async Task<List<Subtitle>> getSubtitle(String imdbCode)
    {
        //list of subtitle
        List<Subtitle> subtitles = new List<Subtitle>();

        //get data
        var response = await _client.GetAsync($"https://yts-subs.com/movie-imdb/{imdbCode}");
        response.EnsureSuccessStatusCode();

        //reponse body
        var responseBody = await response.Content.ReadAsStringAsync();

        //html document
        HtmlDocument htmlDoc = new HtmlDocument();

        //load response body to html document
        htmlDoc.LoadHtml(responseBody);

        //get all high-rating
        var highRating = htmlDoc.DocumentNode.SelectNodes("//tr[@class='high-rating']");

        foreach (var item in highRating)
        {
            //get all link from subtitle-download
            var link = item.SelectSingleNode(".//a[@class='subtitle-download']");

            //get all text from sub-lang
            var lang = item.SelectSingleNode(".//span[@class='sub-lang']");

            //create a formated link that concat link with https://yts-subs.com/
            var formatedLink = $"https://yifysubtitles.org{link.Attributes["href"].Value.Replace("subtitles", "subtitle")}.zip";

            //get text from label rating-cell
            var label = item.SelectSingleNode(".//td[@class='rating-cell']");

            //add new subtitle to list
            subtitles.Add(
                new Subtitle(
                    lang.InnerText,
                    formatedLink,
                    label.InnerText.Trim()
                )
            );
        }

        return subtitles;
    }
}
