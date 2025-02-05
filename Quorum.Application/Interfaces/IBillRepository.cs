using Quorum.Model.Entities;

namespace Quorum.Application.Interfaces;

public interface IBillRepository
{
    Task<IEnumerable<Bill>> GetAllAsync();
    Task<Bill?> GetByIdAsync(int id);
} 