namespace Tests.Entities;

using System.Text.RegularExpressions;

using Domain.Entities;

[TestClass]
public partial class EntitiesTest : TestMethodAttribute
{
    [TestMethod]
    public void CheckMovieEnity()
    {
        Movie movie = new()
        {
            Id = 1,
            Title = "Batman",
            Language = "en",
            Year = 2021,
            Genres = new List<string> { "Action", "Adventure", "Fantasy" },
            YtTrailerCode = "fbS0efVvkVU",
            LargeCoverImage = "test",
            Torrents = new List<Torrent>{new Torrent{
                Url = "torrent url",
                Hash = "torrent hash",
                Quality = "1080p",
                Type = "web",
                Seeds = 1,
                Peers = 1,
                Size = "8.00 GB",
            }},
            ImdbCode = "tt1877830"
        };

        Assert.IsNotNull(movie);
    }

    [TestMethod]
    public void CheckTorrentEnity()
    {
        Torrent torrent = new()
        {
            Url = "torrent url",
            Hash = "torrent hash",
            Quality = "1080p",
            Type = "web",
            Seeds = 1,
            Peers = 1,
            Size = "8.00 GB",
        };

        Assert.IsNotNull(torrent);
        Assert.IsTrue(MyRegex().IsMatch(torrent.GetMagnetLink()));
    }

    [TestMethod]
    public void CheckSubtitleEnity()
    {
        Subtitle subtitle = new()
        {
            Language = "English",
            DownloadLink = "https://yifysubtitles.org/subtitle/test.zip",
            Rating = "10"
        };

        Assert.IsTrue(subtitle.DownloadLink.Contains("yifysubtitles.org"));
    }

    [GeneratedRegex("magnet:\\?xt=urn:btih:[a-zA-Z0-9]*")]
    private static partial Regex MyRegex();
}
