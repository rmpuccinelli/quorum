using System.Globalization;
using System.IO;
using System.Reflection;
using System.Reflection.PortableExecutable;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Quorum.Infrastructure.Configuration;
using Quorum.Infrastructure.Exceptions;

namespace Quorum.Infrastructure.Repositories;

public abstract class BaseCsvRepository<T> where T : class
{
    private readonly CsvFileConfig _config;
    protected IList<T> Cache = [];
    private readonly CsvConfiguration csvConfiguration;

    protected BaseCsvRepository(CsvFileConfig config)
    {
        _config = config;

        csvConfiguration = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.CurrentCulture)
        {
            HasHeaderRecord = true,
            PrepareHeaderForMatch = args => args.Header.ToLower(),
            DetectDelimiter = true,
            TrimOptions = CsvHelper.Configuration.TrimOptions.Trim,                 
        };

    }


    protected virtual void ValidateFileStructure(CsvReader csv)
    {
        try
        {
            // Read the header row
            if (!csv.Read() || !csv.ReadHeader())
            {
                throw new CsvFileStructureException($"CSV file {_config.FileName} is empty or missing a header row.");
            }

            // Ensure required headers are present
            var missingHeaders = ValidateRequiredHeaders(csv);
            if (missingHeaders.Any())
            {
                throw new CsvFileStructureException($"CSV file {_config.FileName} is missing required columns: {string.Join(", ", missingHeaders)}");
            }


            // Validate the first row to check data types
            if (csv.Read())
            {
                try
                {
                    csv.GetRecord<T>();  // Validate type conversion
                }
                catch (TypeConverterException ex)
                {
                    throw new CsvFileStructureException($"Invalid data format in {_config.FileName}: {ex.Message}");
                }
            }

        }
        catch (CsvHelperException ex)
        {
            throw new CsvFileStructureException($"CSV structure validation failed for {_config.FileName}: {ex.Message}");
        }
    }

    /// <summary>
    /// Validates whether all required headers are present in the CSV file.
    /// </summary>
    private static IEnumerable<string> ValidateRequiredHeaders(CsvReader csv)
    {
        var requiredHeaders = typeof(T)
            .GetProperties()
            .Where(x => x.CanRead && x.CanWrite)
            .Select(p => p.Name)
            .ToList();

        var headerRecord = csv.HeaderRecord ?? Array.Empty<string>();
        var missingHeaders = requiredHeaders
            .Where(h => !headerRecord.Contains(h, StringComparer.OrdinalIgnoreCase))
            .ToList();

        return missingHeaders;
    }

    protected async Task<IList<T>> ReadCsvFileAsync()
    {
        if (Cache.Any())
            return Cache;

        var filePath = Path.Combine(_config.FilePath, _config.FileName);
        if (!File.Exists(filePath))
        {
            throw new CsvFileNotFoundException(filePath);
        }

      
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, csvConfiguration);

        InitializeClassMaps(csv);

        try
        {
            await foreach (var record in csv.GetRecordsAsync<T>())
            {
                Cache.Add(record);
            }
        }
        catch 
        {
            throw;
        }


        return Cache;
    }

    private static void InitializeClassMaps(CsvReader csv)
    {
        var classMaps = Assembly.GetExecutingAssembly()
                  .GetTypes()
                  .Where(t => typeof(ClassMap).IsAssignableFrom(t) && !t.IsAbstract)
                  .ToList();

        foreach (var classMap in classMaps)
        {
            var instance = Activator.CreateInstance(classMap) as ClassMap;
            if (instance != null)
            {
                csv.Context.RegisterClassMap(instance);
            }
        }
    }
}