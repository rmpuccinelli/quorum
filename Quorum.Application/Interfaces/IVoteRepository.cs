using Quorum.Model.Entities;

namespace Quorum.Application.Interfaces;

public interface IVoteRepository
{
    Task<IEnumerable<Vote>> GetAllAsync();
} 