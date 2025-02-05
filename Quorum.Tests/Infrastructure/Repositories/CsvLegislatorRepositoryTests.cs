using Quorum.Infrastructure.Configuration;
using Quorum.Infrastructure.Exceptions;
using Quorum.Infrastructure.Repositories;
using Quorum.Model.Entities;
using Xunit;

namespace Quorum.Tests.Infrastructure.Repositories;

public class CsvLegislatorRepositoryTests : BaseCsvRepositoryTests
{
    private const string DefaultFileName = "legislators.csv";
    private readonly LegislatorFileConfig _config;

    public CsvLegislatorRepositoryTests()
    {
        _config = new LegislatorFileConfig(TestDataPath, DefaultFileName);
    }

    [Fact]
    public async Task GetAllAsync_WhenFileExists_ShouldReadAllLegislators()
    {
        // Arrange
        await WriteTestFileAsync(DefaultFileName, 
            "Id,Name\n1,Test Legislator 1\n2,Test Legislator 2");

        var repository = new CsvLegislatorRepository(_config);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        var legislators = result.ToList();
        Assert.Equal(2, legislators.Count);
        Assert.Collection(legislators,
            l =>
            {
                Assert.Equal(1, l.Id);
                Assert.Equal("Test Legislator 1", l.Name);
            },
            l =>
            {
                Assert.Equal(2, l.Id);
                Assert.Equal("Test Legislator 2", l.Name);
            });
    }

    [Fact]
    public async Task GetByIdAsync_WhenLegislatorExists_ShouldReturnLegislator()
    {
        // Arrange
        await WriteTestFileAsync(DefaultFileName, 
            "Id,Name\n1,Test Legislator");

        var repository = new CsvLegislatorRepository(_config);

        // Act
        var result = await repository.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Test Legislator", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_WhenLegislatorDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        await WriteTestFileAsync(DefaultFileName, 
            "Id,Name\n1,Test Legislator");

        var repository = new CsvLegislatorRepository(_config);

        // Act
        var result = await repository.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_WhenFileDoesNotExist_ShouldThrowException()
    {
        // Arrange
        var repository = new CsvLegislatorRepository(_config);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<CsvFileNotFoundException>(
            () => repository.GetAllAsync());
        
        Assert.Equal(Path.Combine(TestDataPath, DefaultFileName), exception.FilePath);
    }

    [Fact]
    public async Task GetAllAsync_WithInvalidDataType_ShouldThrowException()
    {
        // Arrange
        await WriteTestFileAsync(DefaultFileName, 
            "Id,Name\nNotANumber,Test Legislator");

        var repository = new CsvLegislatorRepository(_config);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<CsvHelper.FieldValidationException>(
            () => repository.GetAllAsync());

        Assert.Contains("Field 'NotANumber' is not valid", exception.Message);
    }

    [Fact]
    public async Task GetAllAsync_ShouldCacheResults()
    {
        // Arrange
        await WriteTestFileAsync(DefaultFileName, 
            "Id,Name\n1,Test Legislator");

        var repository = new CsvLegislatorRepository(_config);

        // Act
        var result1 = await repository.GetAllAsync();
        
        // Modify file to verify we're using cached data
        await WriteTestFileAsync(DefaultFileName, 
            "Id,Name\n2,Another Legislator");
        
        var result2 = await repository.GetAllAsync();

        // Assert
        Assert.Same(result1, result2);
        var legislator = Assert.Single(result2);
        Assert.Equal(1, legislator.Id);
        Assert.Equal("Test Legislator", legislator.Name);
    }
} 