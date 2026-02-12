using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<AuthorAward> AuthorAwards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AuthorAward>(entity =>
            {
                entity.ToTable("AuthorAwardBridge");
            });

            modelBuilder.Entity<Author>()
                .Property(a => a.DateOfBirth)
                .HasColumnName("Birthday");

            modelBuilder.Entity<AuthorAward>()
                .HasOne(aa => aa.Author)
                .WithMany(a => a.AuthorAwards)
                .HasForeignKey(aa => aa.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AuthorAward>()
                .HasOne(aa => aa.Award)
                .WithMany(aw => aw.AuthorAwards)
                .HasForeignKey(aa => aa.AwardId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Publisher)
                .WithMany()
                .HasForeignKey(b => b.PublisherId)
                .OnDelete(DeleteBehavior.Restrict);

            // -- seed authors (5)
            modelBuilder.Entity<Author>().HasData(
              new Author { Id = 1, FullName = "George Orwell", Biography = "British writer and journalist.", DateOfBirth = new DateTime(1903, 6, 25, 0, 0, 0, DateTimeKind.Utc) },
              new Author { Id = 2, FullName = "Jane Austen", Biography = "English novelist known for romantic fiction.", DateOfBirth = new DateTime(1775, 12, 16, 0, 0, 0, DateTimeKind.Utc) },
              new Author { Id = 3, FullName = "J.K. Rowling", Biography = "British author best known for Harry Potter series.", DateOfBirth = new DateTime(1965, 7, 31, 0, 0, 0, DateTimeKind.Utc) },
              new Author { Id = 4, FullName = "Mark Twain", Biography = "American writer and humorist.", DateOfBirth = new DateTime(1835, 11, 30, 0, 0, 0, DateTimeKind.Utc) },
              new Author { Id = 5, FullName = "Leo Tolstoy", Biography = "Russian novelist, known for War and Peace.", DateOfBirth = new DateTime(1828, 9, 9, 0, 0, 0, DateTimeKind.Utc) }
            );

            // -- seed publishers (3)
            modelBuilder.Entity<Publisher>().HasData(
              new Publisher { Id = 1, Name = "Penguin Books", Address = "80 Strand, London", Website = "https://penguin.co.uk" },
              new Publisher { Id = 2, Name = "Bloomsbury", Address = "50 Bedford Square, London", Website = "https://www.bloomsbury.com" },
              new Publisher { Id = 3, Name = "Vintage Books", Address = "New York, USA", Website = "https://www.vintagebooks.com" }
            );

            // -- seed books (12)  (obavezno navesti PublishedDate sa Utc)
            modelBuilder.Entity<Book>().HasData(
              new Book { Id = 1, Title = "1984", PageCount = 328, PublishedDate = new DateTime(1949, 6, 8, 0, 0, 0, DateTimeKind.Utc), ISBN = "9780451524935", AuthorId = 1, PublisherId = 1 },
              new Book { Id = 2, Title = "Animal Farm", PageCount = 112, PublishedDate = new DateTime(1945, 8, 17, 0, 0, 0, DateTimeKind.Utc), ISBN = "9780451526342", AuthorId = 1, PublisherId = 1 },
              new Book { Id = 3, Title = "Pride and Prejudice", PageCount = 432, PublishedDate = new DateTime(1813, 1, 28, 0, 0, 0, DateTimeKind.Utc), ISBN = "9780141439518", AuthorId = 2, PublisherId = 1 },
              new Book { Id = 4, Title = "Emma", PageCount = 474, PublishedDate = new DateTime(1815, 12, 23, 0, 0, 0, DateTimeKind.Utc), ISBN = "9780141439587", AuthorId = 2, PublisherId = 3 },
              new Book { Id = 5, Title = "Harry Potter and the Philosopher's Stone", PageCount = 223, PublishedDate = new DateTime(1997, 6, 26, 0, 0, 0, DateTimeKind.Utc), ISBN = "9780747532699", AuthorId = 3, PublisherId = 2 },
              new Book { Id = 6, Title = "Harry Potter and the Chamber of Secrets", PageCount = 251, PublishedDate = new DateTime(1998, 7, 2, 0, 0, 0, DateTimeKind.Utc), ISBN = "9780747538493", AuthorId = 3, PublisherId = 2 },
              new Book { Id = 7, Title = "Harry Potter and the Prisoner of Azkaban", PageCount = 317, PublishedDate = new DateTime(1999, 7, 8, 0, 0, 0, DateTimeKind.Utc), ISBN = "9780747542155", AuthorId = 3, PublisherId = 2 },
              new Book { Id = 8, Title = "Adventures of Huckleberry Finn", PageCount = 366, PublishedDate = new DateTime(1884, 12, 10, 0, 0, 0, DateTimeKind.Utc), ISBN = "9780142437179", AuthorId = 4, PublisherId = 3 },
              new Book { Id = 9, Title = "The Adventures of Tom Sawyer", PageCount = 274, PublishedDate = new DateTime(1876, 6, 1, 0, 0, 0, DateTimeKind.Utc), ISBN = "9780143039563", AuthorId = 4, PublisherId = 1 },
              new Book { Id = 10, Title = "War and Peace", PageCount = 1225, PublishedDate = new DateTime(1869, 1, 1, 0, 0, 0, DateTimeKind.Utc), ISBN = "9780140447934", AuthorId = 5, PublisherId = 3 },
              new Book { Id = 11, Title = "Anna Karenina", PageCount = 864, PublishedDate = new DateTime(1877, 4, 1, 0, 0, 0, DateTimeKind.Utc), ISBN = "9780143035008", AuthorId = 5, PublisherId = 3 },
              new Book { Id = 12, Title = "The Death of Ivan Ilyich", PageCount = 86, PublishedDate = new DateTime(1886, 1, 1, 0, 0, 0, DateTimeKind.Utc), ISBN = "9780553210354", AuthorId = 5, PublisherId = 1 }
            );

            // -- seed awards (4)
            modelBuilder.Entity<Award>().HasData(
              new Award { Id = 1, Name = "Literary Prize A", Description = "Prestigious national award", StartedYear = 1950 },
              new Award { Id = 2, Name = "International Book Award", Description = "International recognition", StartedYear = 1965 },
              new Award { Id = 3, Name = "Readers' Choice", Description = "Award voted by readers", StartedYear = 1990 },
              new Award { Id = 4, Name = "Lifetime Achievement", Description = "For lifetime contribution to literature", StartedYear = 2000 }
            );

            // -- seed author-award bridge (15 rows)
            modelBuilder.Entity<AuthorAward>().HasData(
              new AuthorAward { Id = 1, AuthorId = 1, AwardId = 1, YearAwarded = 1950 },
              new AuthorAward { Id = 2, AuthorId = 1, AwardId = 2, YearAwarded = 1955 },
              new AuthorAward { Id = 3, AuthorId = 2, AwardId = 1, YearAwarded = 1805 },
              new AuthorAward { Id = 4, AuthorId = 2, AwardId = 3, YearAwarded = 1810 },
              new AuthorAward { Id = 5, AuthorId = 3, AwardId = 3, YearAwarded = 2000 },
              new AuthorAward { Id = 6, AuthorId = 3, AwardId = 2, YearAwarded = 2001 },
              new AuthorAward { Id = 7, AuthorId = 3, AwardId = 4, YearAwarded = 2020 },
              new AuthorAward { Id = 8, AuthorId = 4, AwardId = 1, YearAwarded = 1870 },
              new AuthorAward { Id = 9, AuthorId = 4, AwardId = 3, YearAwarded = 1880 },
              new AuthorAward { Id = 10, AuthorId = 4, AwardId = 2, YearAwarded = 1890 },
              new AuthorAward { Id = 11, AuthorId = 5, AwardId = 1, YearAwarded = 1900 },
              new AuthorAward { Id = 12, AuthorId = 5, AwardId = 4, YearAwarded = 1910 },
              new AuthorAward { Id = 13, AuthorId = 1, AwardId = 4, YearAwarded = 1960 },
              new AuthorAward { Id = 14, AuthorId = 2, AwardId = 2, YearAwarded = 1820 },
              new AuthorAward { Id = 15, AuthorId = 5, AwardId = 3, YearAwarded = 1905 }
            );
        }
    }
}
