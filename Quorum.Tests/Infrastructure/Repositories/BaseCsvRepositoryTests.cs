using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Quorum.Infrastructure.Configuration;
using Quorum.Infrastructure.Exceptions;
using Xunit;

namespace Quorum.Tests.Infrastructure.Repositories;

public abstract class BaseCsvRepositoryTests : IDisposable
{
    protected readonly string TestDataPath;

    protected BaseCsvRepositoryTests()
    {
        TestDataPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(TestDataPath);
    }

    protected static CsvConfiguration GetTestCsvConfiguration()
    {
        return new CsvConfiguration(CultureInfo.CurrentCulture)
        {
            HasHeaderRecord = true,
            PrepareHeaderForMatch = args => args.Header.ToLower(),
            DetectDelimiter = true,
            TrimOptions = CsvHelper.Configuration.TrimOptions.Trim
        };
    }

    protected async Task WriteTestFileAsync(string fileName, string content)
    {
        await File.WriteAllTextAsync(
            Path.Combine(TestDataPath, fileName),
            content.Replace("\n", Environment.NewLine));
    }

    public void Dispose()
    {
        if (Directory.Exists(TestDataPath))
        {
            Directory.Delete(TestDataPath, true);
        }
    }
} 