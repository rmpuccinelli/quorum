namespace Quorum.Application.DTOs
{
    public class BillAnalysisDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string SponsorName { get; set; } = string.Empty;
        public int SupportCount { get; set; }
        public int OpposeCount { get; set; }
    }
} 