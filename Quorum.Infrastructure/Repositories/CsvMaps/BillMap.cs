using CsvHelper.Configuration;
using Quorum.Model.Entities;

namespace Quorum.Infrastructure.Repositories.CsvMaps;

public sealed class BillMap : ClassMap<Bill>
{
    public BillMap()
    {
        Map(m => m.Id).Name("id").Validate(field => int.TryParse(field.Field, out _));
        Map(m => m.Title).Name("title").Validate(field => !string.IsNullOrWhiteSpace(field.Field));
        Map(m => m.SponsorId).Name("sponsor_id").Validate(field => int.TryParse(field.Field, out _));
    }
} 