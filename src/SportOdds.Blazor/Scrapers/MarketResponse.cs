using System.Text.Json.Serialization;

namespace SportOdds.Blazor.Scrapers;

public class MarketResponse
{
    [JsonPropertyName("cutoffAt")]
    public DateTime? CutoffAt { get; set; }

    [JsonPropertyName("key")]
    public string? Key { get; set; }

    [JsonPropertyName("limits")]
    public List<LimitResponse> Limits { get; set; } = [];

    [JsonPropertyName("matchupId")]
    public int? MatchupId { get; set; }

    [JsonPropertyName("period")]
    public int? Period { get; set; }

    [JsonPropertyName("prices")]
    public List<PriceResponse> Prices { get; set; } = [];

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("version")]
    public long? Version { get; set; }
}

public class LimitResponse
{
    [JsonPropertyName("amount")]
    public int? Amount { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }
}

public class PriceResponse
{
    [JsonPropertyName("participantId")]
    public int? ParticipantId { get; set; }

    [JsonPropertyName("designation")]
    public string? Designation { get; set; }

    [JsonPropertyName("points")]
    public double? Points { get; set; }

    [JsonPropertyName("price")]
    public int? Price { get; set; }
}
