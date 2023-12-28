using System.Collections.Generic;
using System.Linq;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;
using xamis.Game.Graphics;

namespace xamis.Game.Screens.Menu;

public partial class DisclaimerScreen : Screen
{
    private const float icon_y = -85.0f;
    private const float icon_size = 30.0f;

    private SpriteIcon? icon;
    private Colour4 iconColour;

    private FillFlowContainer? fillFlow;
    private TextFlowContainer? textFlow, supportFlow;

    private readonly List<ITextPart>? expendableText = new List<ITextPart>();

    private readonly Screen? nextScreen;

    public DisclaimerScreen(Screen? nextScreen)
    {
        this.nextScreen = nextScreen;
    }

    [BackgroundDependencyLoader]
    private void load()
    {
        AddRangeInternal(new Drawable[]
        {
            icon = new SpriteIcon
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Icon = FontAwesome.Regular.Heart,
                Size = new Vector2(icon_size),
                Y = icon_y
            },
            fillFlow = new FillFlowContainer
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.TopCentre,
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
                Direction = FillDirection.Vertical,
                Y = icon_y,
                Children = new Drawable[]
                {
                    textFlow = new TextFlowContainer
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        TextAnchor = Anchor.TopCentre,
                        AutoSizeAxes = Axes.Y,
                        Width = 600,
                        Spacing = new Vector2(0, 2)
                    }
                }
            },
            supportFlow = new TextFlowContainer
            {
                Anchor = Anchor.BottomCentre,
                Origin = Anchor.BottomCentre,
                TextAnchor = Anchor.BottomCentre,
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
                Alpha = 0,
                Padding = new MarginPadding(20),
                Spacing = new Vector2(0, 2)
            }
        });

        textFlow.AddText("esse jogo contem ",
            text => text.Font = text.Font.With(GameFontTypeface.Nunito, 30, GameFontWeight.Regular));

        expendableText?.Add(textFlow.AddText("cenas fortes ",
            text => {
                text.Font = text.Font.With(GameFontTypeface.Nunito, 30, GameFontWeight.SemiBold);
                text.Colour = Colour4.Red;
            }
        ));

        static void formatRegular(SpriteText text) =>
            text.Font = GameFont.GetFont(size: 20, weight: GameFontWeight.Regular);
        static void formatSemiBold(SpriteText text) =>
            text.Font = GameFont.GetFont(size: 20, weight: GameFontWeight.SemiBold);

        textFlow.NewParagraph();

        textFlow.AddText("que podem ser ", formatRegular);
        textFlow.AddText("perturbadores", text =>
        {
            text.Font = text.Font.With(GameFontTypeface.Nunito, 20, GameFontWeight.SemiBold);
            text.Colour = Colour4.Red;
        });

        expendableText?.Add(textFlow.AddText(", talvez não seja uma boa ideia jogar!", formatRegular));
        textFlow.AddText(".", formatRegular);

        textFlow.NewParagraph();
        textFlow.NewParagraph();

        textFlow.AddParagraph("dica de hoje: ", formatSemiBold);
        textFlow.AddParagraph("o jogo é de código-aberto, você pode olhar a vontade!", formatRegular);
        textFlow.NewParagraph();

        textFlow.NewParagraph();

        iconColour = Colour4.Red;
    }

    public override void OnEntering(ScreenTransitionEvent e)
    {
        base.OnEntering(e);

        var delay = 500;

        icon.RotateTo(10);
        icon.FadeOut();
        icon.ScaleTo(0.5f);

        icon?.Delay(500).FadeIn(500).ScaleTo(1, 500, Easing.OutQuint);

        using (BeginDelayedSequence(3000))
        {
            icon.FadeColour(iconColour, 200, Easing.OutQuint);
            icon?.MoveToY(icon_y * 1.3f, 500, Easing.OutCirc)
                .RotateTo(-360, 520, Easing.OutQuint)
                .Then()
                .MoveToY(icon_y, 160, Easing.InQuart);

            using (BeginDelayedSequence(520 + 160))
            {
                fillFlow.MoveToOffset(new Vector2(0, 15), 160, Easing.OutQuart);
            }
        }

        supportFlow?.FadeOut().Delay(2000).FadeIn(500);

        foreach (var c in textFlow!.Children)
            c.FadeTo(0.001f).Delay(delay += 20).FadeIn(500);

            this
                .FadeInFromZero(500)
                .Then(5500)
                .FadeOut(250)
                .ScaleTo(0.9f, 250, Easing.InQuint)
                .Finally(d =>
                {
                    if (nextScreen != null)
                        this.Push(nextScreen);
                });
    }
}
