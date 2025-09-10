using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    internal class Book: ISearchable, IBorrowable 
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public BookCategory Category { get; set; }
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }
        public decimal Price { get; set; }
        public DateTime PublishedDate { get; set; }
        public List<string> BorrowedBy { get; set; }

        public Book()
        {
            BorrowedBy = new List<string>();
        }

        public Book(string iSBN, string title, string author, BookCategory category, int totalCopies, decimal price, DateTime publishedDate): this()
        {
            ISBN = iSBN;
            Title = title;
            Author = author;
            Category = category;
            TotalCopies = totalCopies;
            AvailableCopies = totalCopies;
            Price = price;
            PublishedDate = publishedDate;
        }

        public bool MatchesSearch(string searchTerm)
        {
            return Title.ToLower().Contains(searchTerm.ToLower()) || 
                Author.ToLower().Contains(searchTerm.ToLower()) || 
                ISBN.Contains(searchTerm) || 
                Category.ToString().ToLower().Contains(searchTerm.ToLower());
        }

        public bool CanBorrow(Member member)
        {
            return AvailableCopies > 0 && member.CanBorrowMoreBooks();
        }

        public void BorrowBook(Member member)
        {
            if (CanBorrow(member))
            {
                AvailableCopies--;
                BorrowedBy.Add(member.Id);
            }
        }

        public void ReturnBook()
        {
            if (AvailableCopies < TotalCopies)
            {
                AvailableCopies++;
            }
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"ISBN: {ISBN}");
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Author: {Author}");
            Console.WriteLine($"Category: {Category}");
            Console.WriteLine($"Available/Total: {AvailableCopies}/{TotalCopies}");
            Console.WriteLine($"Price: ${Price:F2}");
            Console.WriteLine($"Published: {PublishedDate:yyyy-MM-dd}");
        }
    }
}
