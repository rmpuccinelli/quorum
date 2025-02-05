using CsvHelper.Configuration;
using Quorum.Model.Entities;
using Quorum.Model.Enums;

namespace Quorum.Infrastructure.Repositories.CsvMaps;

public sealed class VoteResultMap : ClassMap<VoteResult>
{
    public VoteResultMap()
    {
        Map(m => m.Id).Name("id").Validate(field => int.TryParse(field.Field, out _));
        Map(m => m.LegislatorId).Name("legislator_id").Validate(field => int.TryParse(field.Field, out _));
        Map(m => m.VoteId).Name("vote_id").Validate(field => int.TryParse(field.Field, out _));
        Map(m => m.VoteType).Name("vote_type").Validate(field => 
        {
            if (!int.TryParse(field.Field, out int value)) return false;
            return Enum.IsDefined(typeof(eVoteType), value);
        });
    }
} 