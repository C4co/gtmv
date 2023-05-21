namespace Tests.Entities;

using System.Text.RegularExpressions;

using Domain.Entities;

[TestClass]
public class EntitiesTest : TestMethodAttribute
{
    [TestMethod]
    public void CheckMovieEnity()
    {
        Movie movie = new Movie
        {
            id = 1,
            title = "Batman",
            language = "en",
            year = 2021,
            genres = new List<string> { "Action", "Adventure", "Fantasy" },
            yt_trailer_code = "fbS0efVvkVU",
            large_cover_image = "test",
            torrents = new List<Torrent>{new Torrent{
                url = "torrent url",
                hash = "torrent hash",
                quality = "1080p",
                type = "web",
                seeds = 1,
                peers = 1,
                size = "8.00 GB",
            }},
            imdb_code = "tt1877830"
        };

        Assert.IsNotNull(movie);
    }

    [TestMethod]
    public void CheckTorrentEnity()
    {
        Torrent torrent = new Torrent
        {
            url = "torrent url",
            hash = "torrent hash",
            quality = "1080p",
            type = "web",
            seeds = 1,
            peers = 1,
            size = "8.00 GB",
        };

        Assert.IsNotNull(torrent);
        Assert.IsTrue(Regex.IsMatch(torrent.getMagnetLink(), @"magnet:\?xt=urn:btih:[a-zA-Z0-9]*"));
    }

    [TestMethod]
    public void CheckSubtitleEnity()
    {
        Subtitle subtitle = new Subtitle
        {
            language = "English",
            downloadLink = "https://yifysubtitles.org/subtitle/test.zip",
            rating = "10"
        };

        Assert.IsTrue(subtitle.downloadLink.Contains("yifysubtitles.org"));
    }
}
