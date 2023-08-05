namespace Tests;

using System.Threading.Tasks;

using Data.Repositories;

[TestClass]
public class Repositories
{
    [TestMethod]
    public async Task MovieRepositoryAsync()
    {
        MovieRepository movieRepository = new(
            httpClient: new HttpClient()
        );

        var reponse = await movieRepository.Search("batman");

        Assert.IsTrue(reponse.Data?.Movies.Count > 0);
        Assert.IsTrue(reponse.Data?.Movies[0] is not null);
    }

    [TestMethod]
    public async Task SubtitleRepositoryAsync()
    {
        SubtitleRepository subtitleRepository = new(
            httpClient: new HttpClient()
        );

        var subtitles = await subtitleRepository.GetSubtitle("tt0372784");

        Assert.IsTrue(subtitles.Count > 0);
        Assert.IsTrue(subtitles[0] is not null);
    }
}
