using CsvHelper;
using Quorum.Application.Interfaces;
using Quorum.Infrastructure.Configuration;
using Quorum.Model.Entities;

namespace Quorum.Infrastructure.Repositories;

public class CsvVoteRepository : BaseCsvRepository<Vote>, IVoteRepository
{
    public CsvVoteRepository(VoteFileConfig config) 
        : base(config)
    {
    }

    public async Task<IEnumerable<Vote>> GetAllAsync()
    {
        return await ReadCsvFileAsync();
    }

} 