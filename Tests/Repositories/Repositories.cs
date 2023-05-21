namespace Tests;

using System.Threading.Tasks;

using Data.Repositories;

using Domain.Entities;

[TestClass]
public class Repositories
{
    [TestMethod]
    public async Task MovieRepositoryAsync()
    {
        MovieRepository movieRepository = new MovieRepository(
            httpClient: new HttpClient()
        );

        var reponse = await movieRepository.Search("batman");

        Assert.IsTrue(reponse.data?.movies.Count > 0);
        Assert.IsTrue(reponse.data?.movies[0] is Movie);
    }

    [TestMethod]
    public async Task SubtitleRepositoryAsync()
    {
        SubtitleRepository subtitleRepository = new SubtitleRepository(
            httpClient: new HttpClient()
        );

        var Subtitles = await subtitleRepository.getSubtitle("tt0372784");

        Assert.IsTrue(Subtitles.Count > 0);
        Assert.IsTrue(Subtitles[0] is Subtitle);
    }
}
