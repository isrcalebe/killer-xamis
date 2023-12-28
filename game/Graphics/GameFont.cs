using osu.Framework.Graphics.Sprites;

namespace xamis.Game.Graphics;

public static class GameFont
{
    public const float DEFAULT_FONT_SIZE = 16;

    public static FontUsage Default => GetFont();

    public static FontUsage Nunito => GetFont(GameFontTypeface.Nunito, weight: GameFontWeight.Regular);

    public static FontUsage Roboto => GetFont(GameFontTypeface.Roboto, weight: GameFontWeight.Regular);

    public static FontUsage GetFont(GameFontTypeface typeface = GameFontTypeface.Nunito, float size = DEFAULT_FONT_SIZE, GameFontWeight weight = GameFontWeight.Medium, bool italics = false, bool fixedWidth = false)
    {
        var familyString = GetFamilyString(typeface);

        return new FontUsage(familyString, size, GetWeightString(familyString, weight), getItalics(italics), fixedWidth);
    }

    public static string GetFamilyString(GameFontTypeface typeface)
    {
        switch (typeface)
        {
            case GameFontTypeface.Nunito:
                return @"Nunito";
        }

            return @"Nunito";
    }

    public static string GetWeightString(string family, GameFontWeight weight)
    {
        return weight.ToString();
    }

    private static bool getItalics(in bool italicsRequested)
    {
        return false;
    }
}

public static class OsuFontExtensions
{
    public static FontUsage With(this FontUsage usage, GameFontTypeface? typeface = null, float? size = null, GameFontWeight? weight = null, bool? italics = null, bool? fixedWidth = null)
    {
        var familyString = typeface != null ? GameFont.GetFamilyString(typeface.Value) : usage.Family;
        var weightString = weight != null ? GameFont.GetWeightString(familyString, weight.Value) : usage.Weight;

        return usage.With(familyString, size, weightString, italics, fixedWidth);
    }
}

public enum GameFontTypeface
{
    Nunito,
    Roboto
}

public enum GameFontWeight
{
    Light = 300,
    Regular = 400,
    Medium = 500,
    SemiBold = 600,
    Bold = 700,
    Black = 900
}
