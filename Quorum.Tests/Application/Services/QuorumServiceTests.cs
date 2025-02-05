using Moq;
using Quorum.Application.DTOs;
using Quorum.Application.Interfaces;
using Quorum.Application.Services;
using Quorum.Model.Entities;
using Quorum.Model.Enums;
using Xunit;

namespace Quorum.Tests.Application.Services;

public class QuorumServiceTests
{
    private readonly Mock<IQuorumUnitOfWork> _unitOfWork;
    private readonly QuorumService _service;

    public QuorumServiceTests()
    {
        _unitOfWork = new Mock<IQuorumUnitOfWork>();
        _service = new QuorumService(_unitOfWork.Object);
    }

    [Fact]
    public async Task GetLegislatorVotingRecords_WithMultipleVotes_ShouldCalculateCorrectCounts()
    {
        // Arrange
        var legislator = new Legislator { Id = 1, Name = "Test Legislator" };
        var voteResults = new[]
        {
            new VoteResult { LegislatorId = 1, VoteType = (int)eVoteType.Yea },
            new VoteResult { LegislatorId = 1, VoteType = (int)eVoteType.Yea },
            new VoteResult { LegislatorId = 1, VoteType = (int)eVoteType.Nay },
            new VoteResult { LegislatorId = 2, VoteType = (int)eVoteType.Yea } // Different legislator
        };

        _unitOfWork.Setup(r => r.Legislators.GetAllAsync())
            .ReturnsAsync(new[] { legislator });
        _unitOfWork.Setup(r => r.VoteResults.GetAllAsync())
            .ReturnsAsync(voteResults);

        // Act
        var result = await _service.GetLegislatorVotingRecordsAsync();

        // Assert
        var record = Assert.Single(result);
        Assert.Equal(legislator.Id, record.LegislatorId);
        Assert.Equal(legislator.Name, record.LegislatorName);
        Assert.Equal(2, record.SupportedBills);
        Assert.Equal(1, record.OpposedBills);
    }

    [Fact]
    public async Task GetLegislatorVotingRecords_WithNoVotes_ShouldReturnZeroCounts()
    {
        // Arrange
        var legislator = new Legislator { Id = 1, Name = "Test Legislator" };

        _unitOfWork.Setup(r => r.Legislators.GetAllAsync())
            .ReturnsAsync(new[] { legislator });
        _unitOfWork.Setup(r => r.VoteResults.GetAllAsync())
            .ReturnsAsync(Array.Empty<VoteResult>());

        // Act
        var result = await _service.GetLegislatorVotingRecordsAsync();

        // Assert
        var record = Assert.Single(result);
        Assert.Equal(0, record.SupportedBills);
        Assert.Equal(0, record.OpposedBills);
    }

    [Fact]
    public async Task GetBillSupportAnalysis_WithMultipleVotes_ShouldCalculateCorrectCounts()
    {
        // Arrange
        var sponsor = new Legislator { Id = 1, Name = "Test Sponsor" };
        var bill = new Bill { Id = 1, Title = "Test Bill", SponsorId = 1 };
        var vote = new Vote { Id = 1, BillId = 1 };
        var voteResults = new[]
        {
            new VoteResult { VoteId = 1, VoteType =(int)eVoteType.Yea },
            new VoteResult { VoteId = 1, VoteType =(int)eVoteType.Yea },
            new VoteResult { VoteId = 1, VoteType =(int)eVoteType.Nay },
            new VoteResult { VoteId = 2, VoteType =(int)eVoteType.Yea } // Different vote
        };

        _unitOfWork.Setup(r => r.Legislators.GetAllAsync())
            .ReturnsAsync(new[] { sponsor });
        _unitOfWork.Setup(r => r.Bills.GetAllAsync())
            .ReturnsAsync(new[] { bill });
        _unitOfWork.Setup(r => r.Votes.GetAllAsync())
            .ReturnsAsync(new[] { vote });
        _unitOfWork.Setup(r => r.VoteResults.GetAllAsync())
            .ReturnsAsync(voteResults);

        // Act
        var result = await _service.GetBillSupportAnalysisAsync();

        // Assert
        var analysis = Assert.Single(result);
        Assert.Equal(bill.Id, analysis.Id);
        Assert.Equal(bill.Title, analysis.Title);
        Assert.Equal(sponsor.Name, analysis.SponsorName);
        Assert.Equal(2, analysis.SupportCount);
        Assert.Equal(1, analysis.OpposeCount);
    }

    [Fact]
    public async Task GetBillSupportAnalysis_WithMissingSponsor_ShouldReturnUnknownSponsor()
    {
        // Arrange
        var bill = new Bill { Id = 1, Title = "Test Bill", SponsorId = 999 };

        _unitOfWork.Setup(r => r.Legislators.GetAllAsync())
            .ReturnsAsync(Array.Empty<Legislator>());
        _unitOfWork.Setup(r => r.Bills.GetAllAsync())
            .ReturnsAsync(new[] { bill });
        _unitOfWork.Setup(r => r.Votes.GetAllAsync())
            .ReturnsAsync(Array.Empty<Vote>());
        _unitOfWork.Setup(r => r.VoteResults.GetAllAsync())
            .ReturnsAsync(Array.Empty<VoteResult>());

        // Act
        var result = await _service.GetBillSupportAnalysisAsync();

        // Assert
        var analysis = Assert.Single(result);
        Assert.Equal("Unknown", analysis.SponsorName);
        Assert.Equal(0, analysis.SupportCount);
        Assert.Equal(0, analysis.OpposeCount);
    }

    [Fact]
    public async Task GetBillSupportAnalysis_WithNoVotes_ShouldReturnZeroCounts()
    {
        // Arrange
        var sponsor = new Legislator { Id = 1, Name = "Test Sponsor" };
        var bill = new Bill { Id = 1, Title = "Test Bill", SponsorId = 1 };

        _unitOfWork.Setup(r => r.Legislators.GetAllAsync())
            .ReturnsAsync(new[] { sponsor });
        _unitOfWork.Setup(r => r.Bills.GetAllAsync())
            .ReturnsAsync(new[] { bill });
        _unitOfWork.Setup(r => r.Votes.GetAllAsync())
            .ReturnsAsync(Array.Empty<Vote>());
        _unitOfWork.Setup(r => r.VoteResults.GetAllAsync())
            .ReturnsAsync(Array.Empty<VoteResult>());

        // Act
        var result = await _service.GetBillSupportAnalysisAsync();

        // Assert
        var analysis = Assert.Single(result);
        Assert.Equal(0, analysis.SupportCount);
        Assert.Equal(0, analysis.OpposeCount);
    }

    [Fact]
    public async Task GetBillSupportAnalysis_WithMultipleBills_ShouldReturnAllAnalyses()
    {
        // Arrange
        var sponsor1 = new Legislator { Id = 1, Name = "Sponsor 1" };
        var sponsor2 = new Legislator { Id = 2, Name = "Sponsor 2" };
        var bills = new[]
        {
            new Bill { Id = 1, Title = "Bill 1", SponsorId = 1 },
            new Bill { Id = 2, Title = "Bill 2", SponsorId = 2 }
        };
        var votes = new[]
        {
            new Vote { Id = 1, BillId = 1 },
            new Vote { Id = 2, BillId = 2 }
        };
        var voteResults = new[]
        {
            new VoteResult { VoteId = 1, VoteType = (int)eVoteType.Yea },
            new VoteResult { VoteId = 2, VoteType = (int)eVoteType.Nay }
        };

        _unitOfWork.Setup(r => r.Legislators.GetAllAsync())
            .ReturnsAsync(new[] { sponsor1, sponsor2 });
        _unitOfWork.Setup(r => r.Bills.GetAllAsync())
            .ReturnsAsync(bills);
        _unitOfWork.Setup(r => r.Votes.GetAllAsync())
            .ReturnsAsync(votes);
        _unitOfWork.Setup(r => r.VoteResults.GetAllAsync())
            .ReturnsAsync(voteResults);

        // Act
        var result = await _service.GetBillSupportAnalysisAsync();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Collection(result,
            a =>
            {
                Assert.Equal("Bill 1", a.Title);
                Assert.Equal("Sponsor 1", a.SponsorName);
                Assert.Equal(1, a.SupportCount);
                Assert.Equal(0, a.OpposeCount);
            },
            a =>
            {
                Assert.Equal("Bill 2", a.Title);
                Assert.Equal("Sponsor 2", a.SponsorName);
                Assert.Equal(0, a.SupportCount);
                Assert.Equal(1, a.OpposeCount);
            });
    }
}