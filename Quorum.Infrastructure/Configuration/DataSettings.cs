namespace Quorum.Infrastructure.Configuration;

public class DataSettings
{
    public string FolderPath { get; set; } = string.Empty;
    public LegislatorDataSettings Legislators { get; set; } = new();
    public BillDataSettings Bills { get; set; } = new();
    public VoteDataSettings Votes { get; set; } = new();
    public VoteResultDataSettings VoteResults { get; set; } = new();
}

public class LegislatorDataSettings
{
    public string FileName { get; set; } = "legislators.csv";
}

public class BillDataSettings
{
    public string FileName { get; set; } = "bills.csv";
}

public class VoteDataSettings
{
    public string FileName { get; set; } = "votes.csv";
}

public class VoteResultDataSettings
{
    public string FileName { get; set; } = "vote_results.csv";
} 