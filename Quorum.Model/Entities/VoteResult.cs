using Quorum.Model.Enums;

namespace Quorum.Model.Entities
{
    public class VoteResult
    {
        public int Id { get; set; }
        public int LegislatorId { get; set; }
        public int VoteId { get; set; }
        public int VoteType { get; set; }        

        public eVoteType GetVoteTypeFromId()
        {
            return VoteType switch
            {
                1 => eVoteType.Yea,
                2 => eVoteType.Nay,                
                _ => eVoteType.NotDefined,
            };            
        }
    }
} 