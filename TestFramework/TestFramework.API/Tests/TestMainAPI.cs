using Microsoft.Playwright;
using System.Threading.Tasks;
using Xunit;

public class MyWebTests : IClassFixture<PlaywrightTestCore>
{
    private readonly PlaywrightTestCore _fixture;

    public MyWebTests(PlaywrightTestCore fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GitHub_Title_ShouldBeCorrect()
    {
        // Access the Page from the fixture
        var page = _fixture.Page;

        await page.GotoAsync("https://github.com/");
        
        // Example assertion
        var title = await page.TitleAsync();
        Assert.Contains("GitHub", title);
    }
}