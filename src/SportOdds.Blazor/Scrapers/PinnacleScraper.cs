using SportOdds.Blazor.Model;
using System.Diagnostics;
using System.Text.Json;

namespace SportOdds.Blazor.Scrapers;

public class PinnacleScraper : HttpClient
{
    public PinnacleScraper()
    {
        DefaultRequestHeaders.Add("x-api-key", "CmX2KcMrXuFmNg6YFbmTxE0y9CIrOi0R");
        BaseAddress = new Uri("https://guest.api.arcadia.pinnacle.com/0.1/leagues/");
    }

    public async Task<List<Game>> GetGamesAsync(string league)
    {
        List<Game> results = [];
        var leagueId = PinnacleOptions.GetLeagueValue(league); 

        var marketString = await GetStringAsync($"https://guest.api.arcadia.pinnacle.com/0.1/leagues/{leagueId}/markets/straight");
        var matchupString = await GetStringAsync($"https://guest.api.arcadia.pinnacle.com/0.1/leagues/{leagueId}/matchups");

        var marketResponse = JsonSerializer.Deserialize<MarketResponse[]>(marketString);
        var matchupResponse = JsonSerializer.Deserialize<MatchupResponse[]>(matchupString);

        if (matchupResponse is null || marketResponse is null)
        {
            return results;
        }

        //Build game objects
        var matchups = matchupResponse.Where(x => x.Type?.ToLower() == "matchup");
        foreach (var matchup in matchups)
        {
            try
            {
                if (matchup is null || matchup.StartTime is null)
                    continue;

                var home = matchup.Participants.FirstOrDefault(x => x.Alignment?.ToLower() == "home");
                var away = matchup.Participants.FirstOrDefault(x => x.Alignment?.ToLower() == "away");

                if (home is null || 
                    away is null || 
                    home.Name is null || 
                    away.Name is null)
                    continue;

                var spreadMarkets = marketResponse.Where(x => x.MatchupId == matchup.Id 
                        && x.Period == 0
                        && x.Type?.ToLower() == "spread" 
                        && x.IsAlternate == false
                        && x.Status?.ToLower() == "open").ToList();


                //To get the proper spread, we need to find the spread market with period of 0 and type spread
                decimal? spread = null;
                string favorite = "N/A";

                var wholeGameSpread = spreadMarkets.FirstOrDefault();

                if (wholeGameSpread is not null)
                {
                    var awayPoints = wholeGameSpread.Prices.FirstOrDefault(x => x.Designation?.ToLower() == "away")?.Points;
                    var homePoints = wholeGameSpread.Prices.FirstOrDefault(x => x.Designation?.ToLower() == "home")?.Points;

                    if (awayPoints is not null && homePoints is not null)
                    {
                        if (awayPoints < homePoints)
                        {
                            favorite = away.Name;
                            spread = Math.Abs((decimal)awayPoints);
                        }
                        else if (homePoints < awayPoints)
                        {
                            favorite = home.Name;
                            spread = Math.Abs((decimal)homePoints);
                        }
                        else
                        {
                            spread = 0;
                        }
                    }
                }

                var localDateTime = new DateTime(matchup.StartTime.Value.Ticks, DateTimeKind.Utc).ToLocalTime();

                var game = new Game()
                {
                    HomeTeam = home.Name,
                    AwayTeam = away.Name,
                    FavoriteTeam = favorite,
                    Spread = spread,
                    GameDateTime = localDateTime,
                };

                //Prevent duplicates
                var duplicateGame = results.FirstOrDefault(x => x.HomeTeam == game.HomeTeam 
                    && x.AwayTeam == game.AwayTeam
                    && x.GameDateTime == game.GameDateTime);

                if (duplicateGame is not null 
                    && duplicateGame.Spread is null
                    && spread is not null)
                {
                    //Update spread
                    duplicateGame.Spread = spread;
                    duplicateGame.FavoriteTeam = favorite;
                }
                else if (duplicateGame is null)
                {
                   //Add new game
                   results.Add(game);
                }
            }
            catch 
            { 
                //Do nothing, any results are better than no results
            }
        }

        results = [.. results.OrderBy(x => x.GameDateTime)];
        return results;
    }
}
