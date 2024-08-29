namespace SportOdds.Blazor.Model
{
    public class Game
    {

        public string HomeTeam { get; set; } = string.Empty;

        public string AwayTeam { get; set; } = string.Empty;
        public string FavoriteTeam { get; set; } = string.Empty;
        public decimal? Spread { get; set; } = null;
        public DateTime GameDateTime { get; set; } = DateTime.MinValue;
        public DateOnly GameDateOnly => DateOnly.FromDateTime(GameDateTime);
    }
}
