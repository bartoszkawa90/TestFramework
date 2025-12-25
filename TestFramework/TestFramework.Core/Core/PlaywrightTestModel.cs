namespace TestFramework.Core;
using Microsoft.Playwright;

namespace TestFramework.Core.Core;

public class PlaywrightTestModel
{
    public IPlaywright Playwright { get; private set; }
    public IBrowser Browser { get; private set; }
    public IBrowserContext Context { get; private set; }
    public IPage Page { get; private set; }
    public IAPIRequestContext APIRequestContext { get; private set; }
}