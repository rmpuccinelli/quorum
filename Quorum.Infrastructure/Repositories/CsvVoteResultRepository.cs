using Quorum.Application.Interfaces;
using Quorum.Infrastructure.Configuration;
using Quorum.Model.Entities;

namespace Quorum.Infrastructure.Repositories;

public class CsvVoteResultRepository : BaseCsvRepository<VoteResult>, IVoteResultRepository
{
    public CsvVoteResultRepository(VoteResultFileConfig config) 
        : base(config)
    {
    }

    public async Task<IEnumerable<VoteResult>> GetAllAsync()
    {
        return await ReadCsvFileAsync();
    }
} 