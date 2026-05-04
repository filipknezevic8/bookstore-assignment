namespace BookstoreApplication.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime IssueReleaseDate { get; set; }
        public required string IssueNumber { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public int ComicVineIssueId { get; set; }
        public int PageCount { get; set; }
        public decimal Price { get; set; }
        public int AvailableCopies { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
