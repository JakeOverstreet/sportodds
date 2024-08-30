using System.Text.Json.Serialization;

namespace SportOdds.Blazor.Scrapers;

public class LeagueResponse
{
    [JsonPropertyName("ageLimit")]
    public int? AgeLimit { get; set; }

    [JsonPropertyName("featureOrder")]
    public int? FeatureOrder { get; set; }

    [JsonPropertyName("group")]
    public string? Group { get; set; }

    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("isFeatured")]
    public bool? IsFeatured { get; set; }

    [JsonPropertyName("isHidden")]
    public bool? IsHidden { get; set; }

    [JsonPropertyName("isPromoted")]
    public bool? IsPromoted { get; set; }

    [JsonPropertyName("isSticky")]
    public bool? IsSticky { get; set; }

    [JsonPropertyName("matchupCount")]
    public int? MatchupCount { get; set; }

    [JsonPropertyName("matchupCountSE")]
    public int? MatchupCountSE { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("sequence")]
    public int? Sequence { get; set; }

    [JsonPropertyName("sport")]
    public SportResponse? Sport { get; set; }
}

public class ParticipantResponse
{
    [JsonPropertyName("alignment")]
    public string? Alignment { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("order")]
    public int? Order { get; set; }

    [JsonPropertyName("stats")]
    public List<StatResponse> Stats { get; set; } = [];
}

public class PeriodResponse
{
    [JsonPropertyName("cutoffAt")]
    public DateTime? CutoffAt { get; set; }

    [JsonPropertyName("hasMoneyline")]
    public bool? HasMoneyline { get; set; }

    [JsonPropertyName("hasSpread")]
    public bool? HasSpread { get; set; }

    [JsonPropertyName("hasTeamTotal")]
    public bool? HasTeamTotal { get; set; }

    [JsonPropertyName("hasTotal")]
    public bool? HasTotal { get; set; }

    [JsonPropertyName("period")]
    public int? Period { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }
}

public class MatchupResponse
{
    [JsonPropertyName("ageLimit")]
    public int? AgeLimit { get; set; }

    [JsonPropertyName("altTeaser")]
    public bool? AltTeaser { get; set; }

    [JsonPropertyName("featureOrder")]
    public int? FeatureOrder { get; set; }

    [JsonPropertyName("hasAltSpread")]
    public bool? HasAltSpread { get; set; }

    [JsonPropertyName("hasAltTotal")]
    public bool? HasAltTotal { get; set; }

    [JsonPropertyName("hasBetshare")]
    public bool? HasBetshare { get; set; }

    [JsonPropertyName("hasLive")]
    public bool? HasLive { get; set; }

    [JsonPropertyName("hasMarkets")]
    public bool? HasMarkets { get; set; }

    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("isBetshareEnabled")]
    public bool? IsBetshareEnabled { get; set; }

    [JsonPropertyName("isFeatured")]
    public bool? IsFeatured { get; set; }

    [JsonPropertyName("isHighlighted")]
    public bool? IsHighlighted { get; set; }

    [JsonPropertyName("isLive")]
    public bool? IsLive { get; set; }

    [JsonPropertyName("isPromoted")]
    public bool? IsPromoted { get; set; }

    [JsonPropertyName("league")]
    public LeagueResponse? League { get; set; }

    [JsonPropertyName("liveMode")]
    public object? LiveMode { get; set; }

    [JsonPropertyName("parent")]
    public object? Parent { get; set; }

    [JsonPropertyName("parentId")]
    public object? ParentId { get; set; }

    [JsonPropertyName("parlayRestriction")]
    public string? ParlayRestriction { get; set; }

    [JsonPropertyName("participants")]
    public List<ParticipantResponse> Participants { get; set; } = [];

    [JsonPropertyName("periods")]
    public List<PeriodResponse> Periods { get; set; } = [];

    [JsonPropertyName("rotation")]
    public int? Rotation { get; set; }

    [JsonPropertyName("startTime")]
    public DateTime? StartTime { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("totalMarketCount")]
    public int? TotalMarketCount { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("units")]
    public string? Units { get; set; }

    [JsonPropertyName("version")]
    public int? Version { get; set; }
}

public class SportResponse
{
    [JsonPropertyName("featureOrder")]
    public int? FeatureOrder { get; set; }

    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("isFeatured")]
    public bool? IsFeatured { get; set; }

    [JsonPropertyName("isHidden")]
    public bool? IsHidden { get; set; }

    [JsonPropertyName("isSticky")]
    public bool? IsSticky { get; set; }

    [JsonPropertyName("matchupCount")]
    public int? MatchupCount { get; set; }

    [JsonPropertyName("matchupCountSE")]
    public int? MatchupCountSE { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("primaryMarketType")]
    public string? PrimaryMarketType { get; set; }
}

public class StatResponse
{
    [JsonPropertyName("period")]
    public int? Period { get; set; }
}

