using Quorum.Application.Interfaces;

namespace Quorum.Infrastructure.Repositories;

public class QuorumUnitOfWork : IQuorumUnitOfWork
{
    public ILegislatorRepository Legislators { get; }
    public IBillRepository Bills { get; }
    public IVoteRepository Votes { get; }
    public IVoteResultRepository VoteResults { get; }

    public QuorumUnitOfWork(
        ILegislatorRepository legislators,
        IBillRepository bills,
        IVoteRepository votes,
        IVoteResultRepository voteResults)
    {
        Legislators = legislators;
        Bills = bills;
        Votes = votes;
        VoteResults = voteResults;
    }
} 