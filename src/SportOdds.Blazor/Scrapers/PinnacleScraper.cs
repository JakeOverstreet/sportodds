using SportOdds.Blazor.Model;
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
        try
        {
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
            var matchups = matchupResponse.Where(x => x.Type.ToLower() == "matchup");
            foreach (var matchup in matchups)
            {
                var home = matchup.Participants.FirstOrDefault(x => x.Alignment.ToLower() == "home");
                var away = matchup.Participants.FirstOrDefault(x => x.Alignment.ToLower() == "away");

                if (home is null || away is null)
                    continue;

                var spreadMarkets = marketResponse.Where(x => x.MatchupId == matchup.Id)
                    .Where(x => x.Type.ToLower() == "spread");


                //To get the proper spread, we need to find the spread market with period of 0 and type spread
                decimal? spread = null;
                string favorite = "N/A";

                var wholeGameSpread = spreadMarkets.FirstOrDefault(x => x.Period == 0 && x.Type.ToLower() == "spread");

                if (wholeGameSpread is not null)
                {
                    var awayPoints = wholeGameSpread.Prices.FirstOrDefault(x => x.Designation.ToLower() == "away")?.Points;
                    var homePoints = wholeGameSpread.Prices.FirstOrDefault(x => x.Designation.ToLower() == "home")?.Points;

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

                var game = new Game()
                {
                    HomeTeam = home.Name,
                    AwayTeam = away.Name,
                    FavoriteTeam = favorite,
                    Spread = spread,
                    GameDateTime = matchup.StartTime,
                };

                results.Add(game);
            }

            results = results.OrderBy(x => x.GameDateTime).ToList();
        }
        catch
        {
            //Do nothing
        }
        return results;
    }
}
