namespace Quorum.Application.DTOs;

public class LegislatorAnalysisDto
{
    public int LegislatorId { get; set; }
    public string LegislatorName { get; set; } = string.Empty;
    public int SupportedBills { get; set; }
    public int OpposedBills { get; set; }
} 