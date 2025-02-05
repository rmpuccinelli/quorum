using Quorum.Infrastructure.Configuration;
using Quorum.Infrastructure.Exceptions;
using Quorum.Infrastructure.Repositories;
using Quorum.Model.Entities;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Quorum.Tests.Infrastructure.Repositories;

public class CsvBillRepositoryTests : BaseCsvRepositoryTests
{
    private const string DefaultFileName = "bills.csv";
    private readonly BillFileConfig _config;

    public CsvBillRepositoryTests()
    {
        _config = new BillFileConfig(TestDataPath, DefaultFileName);
    }

    [Fact]
    public async Task GetAllAsync_WhenFileExists_ShouldReadAllBills()
    {
        // Arrange
        await WriteTestFileAsync(DefaultFileName,
            "Id,Title,sponsor_id\n1,Test Bill 1,1\n2,Test Bill 2,2");

        var repository = new CsvBillRepository(_config);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        var bills = result.ToList();
        Assert.Equal(2, bills.Count);
        Assert.Collection(bills,
            b =>
            {
                Assert.Equal(1, b.Id);
                Assert.Equal("Test Bill 1", b.Title);
                Assert.Equal(1, b.SponsorId);
            },
            b =>
            {
                Assert.Equal(2, b.Id);
                Assert.Equal("Test Bill 2", b.Title);
                Assert.Equal(2, b.SponsorId);
            });
    }

    [Fact]
    public async Task GetByIdAsync_WhenBillExists_ShouldReturnBill()
    {
        // Arrange
        await WriteTestFileAsync(DefaultFileName,
            "Id,Title,sponsor_id\n1,Test Bill,1");

        var repository = new CsvBillRepository(_config);

        // Act
        var result = await repository.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Test Bill", result.Title);
        Assert.Equal(1, result.SponsorId);
    }

    [Fact]
    public async Task GetByIdAsync_WhenBillDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        await WriteTestFileAsync(DefaultFileName,
            "Id,Title,sponsor_id\n1,Test Bill,1");

        var repository = new CsvBillRepository(_config);

        // Act
        var result = await repository.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_WhenFileDoesNotExist_ShouldThrowException()
    {
        // Arrange
        var repository = new CsvBillRepository(_config);

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
            "Id,Title,sponsor_id\nNotANumber,Test Bill,1");

        var repository = new CsvBillRepository(_config);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<CsvHelper.FieldValidationException>(
            () => repository.GetAllAsync());
        
        Assert.Contains("Field 'NotANumber' is not valid", exception.Message);
    }

} 