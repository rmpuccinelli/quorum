using Quorum.Model.Entities;

namespace Quorum.Application.Interfaces;

public interface ILegislatorRepository
{
    Task<IEnumerable<Legislator>> GetAllAsync();
    Task<Legislator?> GetByIdAsync(int id);
} 