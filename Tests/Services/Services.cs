namespace Tests;

using System.Threading.Tasks;

using Data.Services;

[TestClass]
public class Services
{
    [TestMethod]
    public async Task MovieServiceAsync()
    {
        MovieService movieService = new(
            httpClient: new HttpClient()
        );

        var reponse = await movieService.Search("batman");

        Assert.IsTrue(reponse.Data?.Movies.Count > 0);
        Assert.IsTrue(reponse.Data?.Movies[0] is not null);
    }

    [TestMethod]
    public async Task SubtitleServiceAsync()
    {
        SubtitleService subtitleService = new(
            httpClient: new HttpClient()
        );

        var subtitles = await subtitleService.GetSubtitle("tt0372784");

        Assert.IsTrue(subtitles.Count > 0);
        Assert.IsTrue(subtitles[0] is not null);
    }
}
