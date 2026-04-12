using BookstoreApplication.Models;

namespace BookstoreApplication.Utils
{
    public class BookSortTypeOption
    {
        public int Key { get; set; }
        public string Name { get; set; }

        public BookSortTypeOption(BookSortType sortType)
        {
            Key = (int)sortType;
            Name = sortType.ToString();
        }
    }
}
