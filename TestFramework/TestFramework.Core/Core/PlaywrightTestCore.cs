using Microsoft.Playwright;
using System.Threading.Tasks;
using Xunit;

namespace TestFramework.Core.Core;

public class PlaywrightTestCore : IAsyncLifetime
{
    public IPlaywright Playwright { get; private set; }
    public IBrowser Browser { get; private set; }
    public IBrowserContext Context { get; private set; }
    public IPage Page { get; private set; }

    public async Task InitializeAsync()
    {
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();

        Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false, 
            SlowMo = 50
        });

        Context = await Browser.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = new ViewportSize { Width = 1920, Height = 1080 }
        });

        Page = await Context.NewPageAsync();
    }

    public async Task DisposeAsync()
    {
        if (Browser != null) await Browser.CloseAsync();
        Playwright?.Dispose();
    }
}