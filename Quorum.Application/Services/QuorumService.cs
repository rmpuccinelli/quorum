using Quorum.Application.DTOs;
using Quorum.Application.Interfaces;
using Quorum.Model.Entities;
using Quorum.Model.Enums;

namespace Quorum.Application.Services
{
    public class QuorumService : IQuorumService
    {
        private readonly IQuorumUnitOfWork _unitOfWork;

        public QuorumService(IQuorumUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<LegislatorAnalysisDto>> GetLegislatorVotingRecordsAsync()
        {
            var legislators = await _unitOfWork.Legislators.GetAllAsync();
            var voteResults = await _unitOfWork.VoteResults.GetAllAsync();

            return legislators.Select(legislator =>
            {
                var legislatorVotes = voteResults.Where(vr => vr.LegislatorId == legislator.Id);

                return new LegislatorAnalysisDto
                {
                    LegislatorId = legislator.Id,
                    LegislatorName = legislator.Name,
                    SupportedBills = legislatorVotes.Count(vr => vr.VoteType == (int)eVoteType.Yea),
                    OpposedBills = legislatorVotes.Count(vr => vr.VoteType == (int)eVoteType.Nay)
                };
            });
        }

        public async Task<IEnumerable<BillAnalysisDto>> GetBillSupportAnalysisAsync()
        {
            var bills = await _unitOfWork.Bills.GetAllAsync();
            var votes = await _unitOfWork.Votes.GetAllAsync();
            var voteResults = await _unitOfWork.VoteResults.GetAllAsync();
            var legislators = await _unitOfWork.Legislators.GetAllAsync();

            // Pre-index votes and voteResults for fast lookup
            var votesByBill = votes.GroupBy(v => v.BillId).ToDictionary(g => g.Key, g => g.ToList());
            var voteResultsByVote = voteResults.GroupBy(vr => vr.VoteId).ToDictionary(g => g.Key, g => g.ToList());
            var legislatorsDict = legislators.ToDictionary(l => l.Id, l => l.Name);

            return bills.Select(bill =>
            {
                var billVotes = votesByBill.ContainsKey(bill.Id)
                    ? votesByBill[bill.Id].SelectMany(v => voteResultsByVote.GetValueOrDefault(v.Id, new List<VoteResult>()))
                    : Enumerable.Empty<VoteResult>();

                return new BillAnalysisDto
                {
                    Id = bill.Id,
                    Title = bill.Title,
                    SponsorName = legislatorsDict.GetValueOrDefault(bill.SponsorId, "Unknown"),
                    SupportCount = billVotes.Count(vr => vr.VoteType == (int)eVoteType.Yea),
                    OpposeCount = billVotes.Count(vr => vr.VoteType == (int)eVoteType.Nay)
                };
            });
        }
    }
}