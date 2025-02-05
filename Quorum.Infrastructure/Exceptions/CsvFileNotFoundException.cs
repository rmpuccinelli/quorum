namespace Quorum.Infrastructure.Exceptions;

public class CsvFileNotFoundException : Exception
{
    public string FilePath { get; }

    public CsvFileNotFoundException(string filePath)
        : base($"CSV file not found: {filePath}")
    {
        FilePath = filePath;
    }
} 