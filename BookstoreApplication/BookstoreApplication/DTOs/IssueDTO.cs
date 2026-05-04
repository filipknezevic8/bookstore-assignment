namespace BookstoreApplication.DTOs
{
    public class IssueDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? IssueNumber { get; set; }
        public string? CoverDate { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}
