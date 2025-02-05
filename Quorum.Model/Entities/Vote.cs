namespace Quorum.Model.Entities
{
    public class Vote
    {
        public int Id { get; set; }
        public int BillId { get; set; }
        public virtual Bill? Bill { get; set; }
        public virtual ICollection<VoteResult> VoteResults { get; set; } = new List<VoteResult>();
    }
} 