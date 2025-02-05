using Quorum.Application.Interfaces;
using Quorum.Infrastructure.Configuration;
using Quorum.Model.Entities;
using CsvHelper;
using Quorum.Infrastructure.Repositories.CsvMaps;

namespace Quorum.Infrastructure.Repositories;

public class CsvBillRepository : BaseCsvRepository<Bill>, IBillRepository
{
    public CsvBillRepository(BillFileConfig config) 
        : base(config)
    {
    }

    public async Task<IEnumerable<Bill>> GetAllAsync()
    {
        return await ReadCsvFileAsync();
    }

    public async Task<Bill?> GetByIdAsync(int id)
    {
        var bills = await GetAllAsync();
        return bills.FirstOrDefault(b => b.Id == id);
    }
} 