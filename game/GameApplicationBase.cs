global using osu.Framework.Allocation;
global using osu.Framework.Bindables;
global using osu.Framework.Graphics;
global using osu.Framework.Logging;
global using osu.Framework.Utils;
global using osuTK;
using osu.Framework.IO.Stores;
using xamis.Game.Resources;

namespace xamis.Game;

public abstract partial class GameApplicationBase : osu.Framework.Game
{
    private DependencyContainer? dependencies;

    [BackgroundDependencyLoader]
    private void load()
    {
        Resources.AddStore(new DllResourceStore(GameResources.Assembly));

        AddFont(Resources, @"Fonts/Nunito");
        AddFont(Resources, @"Fonts/Nunito-SemiBold");

        Host.Window.Title = "KILLER XAMIS";
    }

    protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent)
        => dependencies = new DependencyContainer(base.CreateChildDependencies(parent));
}
