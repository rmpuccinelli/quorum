using Quorum.Application.DTOs;

namespace Quorum.Application.Interfaces;

public interface IQuorumService
{
    Task<IEnumerable<LegislatorAnalysisDto>> GetLegislatorVotingRecordsAsync();
    Task<IEnumerable<BillAnalysisDto>> GetBillSupportAnalysisAsync();
} 