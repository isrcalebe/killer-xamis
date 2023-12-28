using osu.Framework.Screens;
using xamis.Game.Screens.Menu;

namespace xamis.Game;

public partial class GameApplication : GameApplicationBase
{
    private DependencyContainer? dependencies;

    private ScreenStack? screens;

    [BackgroundDependencyLoader]
    private void load()
    {
        Add(screens = new ScreenStack());

        screens.Push(new DisclaimerScreen(null));
    }

    protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent)
        => dependencies = new DependencyContainer(base.CreateChildDependencies(parent));
}
