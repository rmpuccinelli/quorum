using Quorum.Infrastructure.Configuration;
using Quorum.Infrastructure.Exceptions;
using Quorum.Infrastructure.Repositories;
using Quorum.Model.Entities;
using Quorum.Model.Enums;
using Xunit;

namespace Quorum.Tests.Infrastructure.Repositories;

public class CsvVoteResultRepositoryTests : BaseCsvRepositoryTests
{
    private const string DefaultFileName = "vote_results.csv";
    private readonly VoteResultFileConfig _config;

    public CsvVoteResultRepositoryTests()
    {
        _config = new VoteResultFileConfig(TestDataPath, DefaultFileName);
    }

    [Fact]
    public async Task GetAllAsync_WhenFileExists_ShouldReadAllVoteResults()
    {
        // Arrange
        await WriteTestFileAsync(DefaultFileName,
            "Id,legislator_id,vote_Id,vote_type\n1,1,1,1\n2,2,1,2");

        var repository = new CsvVoteResultRepository(_config);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        var voteResults = result.ToList();
        Assert.Equal(2, voteResults.Count);
        Assert.Collection(voteResults,
            vr =>
            {
                Assert.Equal(1, vr.Id);
                Assert.Equal(1, vr.LegislatorId);
                Assert.Equal(1, vr.VoteId);
                Assert.Equal(eVoteType.Yea, vr.GetVoteTypeFromId());
            },
            vr =>
            {
                Assert.Equal(2, vr.Id);
                Assert.Equal(2, vr.LegislatorId);
                Assert.Equal(1, vr.VoteId);
                Assert.Equal(eVoteType.Nay, vr.GetVoteTypeFromId());
            });
    }

    [Fact]
    public async Task GetAllAsync_WhenFileDoesNotExist_ShouldThrowException()
    {
        // Arrange
        var repository = new CsvVoteResultRepository(_config);

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
            "Id,legislator_id,vote_Id,vote_type\nNotANumber,1,1,1");

        var repository = new CsvVoteResultRepository(_config);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<CsvHelper.FieldValidationException>(
            () => repository.GetAllAsync());

        Assert.Contains("Field 'NotANumber' is not valid", exception.Message);
    }

    [Fact]
    public async Task GetAllAsync_WithInvalidVoteType_ShouldThrowException()
    {
        // Arrange
        await WriteTestFileAsync(DefaultFileName,
            "Id,legislator_id,vote_Id,vote_type\n1,1,1,3");

        var repository = new CsvVoteResultRepository(_config);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<CsvHelper.FieldValidationException>(
            () => repository.GetAllAsync());

        Assert.Contains("Field '3' is not valid", exception.Message);
    }

    [Fact]
    public async Task GetAllAsync_ShouldCacheResults()
    {
        // Arrange
        await WriteTestFileAsync(DefaultFileName, 
            "Id,legislator_id,vote_Id,vote_type\n1,1,1,1");

        var repository = new CsvVoteResultRepository(_config);

        // Act
        var result1 = await repository.GetAllAsync();
        
        await WriteTestFileAsync(DefaultFileName,
            "Id,legislator_id,vote_Id,vote_type\n2,2,2,2");
        
        var result2 = await repository.GetAllAsync();

        // Assert
        Assert.Same(result1, result2);
        var voteResult = Assert.Single(result2);
        Assert.Equal(1, voteResult.Id);
        Assert.Equal(1, voteResult.LegislatorId);
        Assert.Equal(1, voteResult.VoteId);
        Assert.Equal(eVoteType.Yea, voteResult.GetVoteTypeFromId());
    }
} 