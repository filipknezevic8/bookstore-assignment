namespace BookstoreApplication.DTOs
{
    public class SaveIssueDTO
    {
        public string Name { get; set; }
        public DateTime IssueReleaseDate { get; set; }
        public string IssueNumber { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public int ComicVineIssueId { get; set; }
        public int PageCount { get; set; }
        public decimal Price { get; set; }
        public int AvailableCopies { get; set; }
    }
}
