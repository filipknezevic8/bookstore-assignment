using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookstoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Biography", "Birthday", "FullName" },
                values: new object[,]
                {
                    { 1, "British writer and journalist.", new DateTime(1903, 6, 25, 0, 0, 0, 0, DateTimeKind.Utc), "George Orwell" },
                    { 2, "English novelist known for romantic fiction.", new DateTime(1775, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Jane Austen" },
                    { 3, "British author best known for Harry Potter series.", new DateTime(1965, 7, 31, 0, 0, 0, 0, DateTimeKind.Utc), "J.K. Rowling" },
                    { 4, "American writer and humorist.", new DateTime(1835, 11, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Mark Twain" },
                    { 5, "Russian novelist, known for War and Peace.", new DateTime(1828, 9, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Leo Tolstoy" }
                });

            migrationBuilder.InsertData(
                table: "Awards",
                columns: new[] { "Id", "Description", "Name", "StartedYear" },
                values: new object[,]
                {
                    { 1, "Prestigious national award", "Literary Prize A", 1950 },
                    { 2, "International recognition", "International Book Award", 1965 },
                    { 3, "Award voted by readers", "Readers' Choice", 1990 },
                    { 4, "For lifetime contribution to literature", "Lifetime Achievement", 2000 }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Address", "Name", "Website" },
                values: new object[,]
                {
                    { 1, "80 Strand, London", "Penguin Books", "https://penguin.co.uk" },
                    { 2, "50 Bedford Square, London", "Bloomsbury", "https://www.bloomsbury.com" },
                    { 3, "New York, USA", "Vintage Books", "https://www.vintagebooks.com" }
                });

            migrationBuilder.InsertData(
                table: "AuthorAwardBridge",
                columns: new[] { "Id", "AuthorId", "AwardId", "YearAwarded" },
                values: new object[,]
                {
                    { 1, 1, 1, 1950 },
                    { 2, 1, 2, 1955 },
                    { 3, 2, 1, 1805 },
                    { 4, 2, 3, 1810 },
                    { 5, 3, 3, 2000 },
                    { 6, 3, 2, 2001 },
                    { 7, 3, 4, 2020 },
                    { 8, 4, 1, 1870 },
                    { 9, 4, 3, 1880 },
                    { 10, 4, 2, 1890 },
                    { 11, 5, 1, 1900 },
                    { 12, 5, 4, 1910 },
                    { 13, 1, 4, 1960 },
                    { 14, 2, 2, 1820 },
                    { 15, 5, 3, 1905 }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "ISBN", "PageCount", "PublishedDate", "PublisherId", "Title" },
                values: new object[,]
                {
                    { 1, 1, "9780451524935", 328, new DateTime(1949, 6, 8, 0, 0, 0, 0, DateTimeKind.Utc), 1, "1984" },
                    { 2, 1, "9780451526342", 112, new DateTime(1945, 8, 17, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Animal Farm" },
                    { 3, 2, "9780141439518", 432, new DateTime(1813, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Pride and Prejudice" },
                    { 4, 2, "9780141439587", 474, new DateTime(1815, 12, 23, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Emma" },
                    { 5, 3, "9780747532699", 223, new DateTime(1997, 6, 26, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Harry Potter and the Philosopher's Stone" },
                    { 6, 3, "9780747538493", 251, new DateTime(1998, 7, 2, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Harry Potter and the Chamber of Secrets" },
                    { 7, 3, "9780747542155", 317, new DateTime(1999, 7, 8, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Harry Potter and the Prisoner of Azkaban" },
                    { 8, 4, "9780142437179", 366, new DateTime(1884, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Adventures of Huckleberry Finn" },
                    { 9, 4, "9780143039563", 274, new DateTime(1876, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "The Adventures of Tom Sawyer" },
                    { 10, 5, "9780140447934", 1225, new DateTime(1869, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "War and Peace" },
                    { 11, 5, "9780143035008", 864, new DateTime(1877, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Anna Karenina" },
                    { 12, 5, "9780553210354", 86, new DateTime(1886, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "The Death of Ivan Ilyich" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AuthorAwardBridge",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Awards",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
