using Quorum.Application.Interfaces;
using Quorum.Infrastructure.Configuration;
using Quorum.Model.Entities;
using CsvHelper;
using Quorum.Infrastructure.Repositories.CsvMaps;

namespace Quorum.Infrastructure.Repositories;

public class CsvLegislatorRepository : BaseCsvRepository<Legislator>, ILegislatorRepository
{
    public CsvLegislatorRepository(LegislatorFileConfig config) 
        : base(config)
    {
    }

    public async Task<IEnumerable<Legislator>> GetAllAsync()
    {
        return await ReadCsvFileAsync();
    }

    public async Task<Legislator?> GetByIdAsync(int id)
    {
        var legislators = await GetAllAsync();
        return legislators.FirstOrDefault(l => l.Id == id);
    }
} 