namespace Quorum.Infrastructure.Configuration;

public class LegislatorFileConfig : CsvFileConfig
{
    public LegislatorFileConfig(string filePath, string fileName) : base(filePath, fileName) { }
}

public class BillFileConfig : CsvFileConfig
{
    public BillFileConfig(string filePath, string fileName) : base(filePath, fileName) { }
}

public class VoteFileConfig : CsvFileConfig
{
    public VoteFileConfig(string filePath, string fileName) : base(filePath, fileName) { }
}

public class VoteResultFileConfig : CsvFileConfig
{
    public VoteResultFileConfig(string filePath, string fileName) : base(filePath, fileName) { }
} 