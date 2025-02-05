namespace Quorum.Application.Interfaces;

public interface IQuorumUnitOfWork
{
    ILegislatorRepository Legislators { get; }
    IBillRepository Bills { get; }
    IVoteRepository Votes { get; }
    IVoteResultRepository VoteResults { get; }
} 