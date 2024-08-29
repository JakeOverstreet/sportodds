namespace SportOdds.Blazor.Scrapers;

public static class PinnacleOptions
{
    public const string NBA = "487";
    public const string NFL = "889";
    public const string NHL = "1456";
    public const string CFB = "880";
    public const string CBB = "493";

    public static string GetLeagueValue(string leagueName)
    {
        switch (leagueName.ToUpper())
        {
            case nameof(NBA):
                return NBA;
            case nameof(NFL):
                return NFL;
            case nameof(NHL):
                return NHL;
            case nameof(CBB):
                return CBB;
            case nameof(CFB):
            default:
                return CFB;
        }
    }
}
