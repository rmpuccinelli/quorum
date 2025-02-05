using CsvHelper.Configuration;
using Quorum.Model.Entities;

namespace Quorum.Infrastructure.Repositories.CsvMaps;

public sealed class VoteMap : ClassMap<Vote>
{
    public VoteMap()
    {
        Map(m => m.Id).Name("id").Validate(field => int.TryParse(field.Field, out _));
        Map(m => m.BillId).Name("bill_id").Validate(field => int.TryParse(field.Field, out _));
    }
} 