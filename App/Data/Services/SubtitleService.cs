using Domain.Entities;

using HtmlAgilityPack;

namespace Data.Services
{
    public class SubtitleService
    {
        private readonly HttpClient _client;

        public SubtitleService(HttpClient httpClient)
        {
            _client = httpClient;
        }

        public async Task<List<Subtitle>> GetSubtitle(string imdbCode)
        {
            List<Subtitle> subtitles = new();

            var response = await _client.GetAsync($"https://yts-subs.com/movie-imdb/{imdbCode}");
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();

            HtmlDocument htmlDoc = new();

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
                        Language = lang.InnerText,
                        DownloadLink = formatedLink,
                        Rating = label.InnerText.Trim()
                    }
                );
            }

            return subtitles;
        }
    }
}