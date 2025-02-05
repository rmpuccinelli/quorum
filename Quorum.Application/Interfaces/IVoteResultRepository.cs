using Quorum.Model.Entities;

namespace Quorum.Application.Interfaces;

public interface IVoteResultRepository
{
    Task<IEnumerable<VoteResult>> GetAllAsync();
} 