namespace Quorum.Model.Entities
{
    public class Bill
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int SponsorId { get; set; }        
    }
} 