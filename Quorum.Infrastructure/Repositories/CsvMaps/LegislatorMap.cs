using CsvHelper.Configuration;
using Quorum.Model.Entities;

namespace Quorum.Infrastructure.Repositories.CsvMaps;

public sealed class LegislatorMap : ClassMap<Legislator>
{
    public LegislatorMap()
    {
        Map(m => m.Id).Name("id").Validate(field => int.TryParse(field.Field, out _));
        Map(m => m.Name).Name("name").Validate(field => !string.IsNullOrWhiteSpace(field.Field));
    }
} 