namespace Quorum.Infrastructure.Configuration;

public class CsvFileConfig
{
    public string FilePath { get; }
    public string FileName { get; }

    public CsvFileConfig(string filePath, string fileName)
    {
        FilePath = filePath;
        FileName = fileName;
    }
} 