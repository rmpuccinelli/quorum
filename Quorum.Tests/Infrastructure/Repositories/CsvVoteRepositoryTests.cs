using Quorum.Infrastructure.Configuration;
using Quorum.Infrastructure.Exceptions;
using Quorum.Infrastructure.Repositories;
using Quorum.Model.Entities;
using Xunit;

namespace Quorum.Tests.Infrastructure.Repositories;

public class CsvVoteRepositoryTests : BaseCsvRepositoryTests
{
    private const string DefaultFileName = "votes.csv";
    private readonly VoteFileConfig _config;

    public CsvVoteRepositoryTests()
    {
        _config = new VoteFileConfig(TestDataPath, DefaultFileName);
    }

    [Fact]
    public async Task GetAllAsync_WhenFileExists_ShouldReadAllVotes()
    {
        // Arrange
        await WriteTestFileAsync(DefaultFileName,
            "Id,bill_id\n1,1\n2,2");

        var repository = new CsvVoteRepository(_config);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        var votes = result.ToList();
        Assert.Equal(2, votes.Count);
        Assert.Collection(votes,
            v =>
            {
                Assert.Equal(1, v.Id);
                Assert.Equal(1, v.BillId);
            },
            v =>
            {
                Assert.Equal(2, v.Id);
                Assert.Equal(2, v.BillId);
            });
    }

    [Fact]
    public async Task GetAllAsync_WhenFileDoesNotExist_ShouldThrowException()
    {
        // Arrange
        var repository = new CsvVoteRepository(_config);

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
            "Id,bill_id\nNotANumber,1");

        var repository = new CsvVoteRepository(_config);

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
            "Id,bill_id\n1,1");

        var repository = new CsvVoteRepository(_config);

        // Act
        var result1 = await repository.GetAllAsync();
        
        await WriteTestFileAsync(DefaultFileName,
            "Id,bill_id\n2,2");
        
        var result2 = await repository.GetAllAsync();

        // Assert
        Assert.Same(result1, result2);
        var vote = Assert.Single(result2);
        Assert.Equal(1, vote.Id);
        Assert.Equal(1, vote.BillId);
    }
} 