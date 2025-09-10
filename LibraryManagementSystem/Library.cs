using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    internal class Library
    {
        private List<Book> books;
        private List<Member> members;
        private List<Transaction> transactions;
        private const string BooksFile = "books.json";
        private const string MembersFile = "members.json";
        private const string TransactionsFile = "transactions.json";

        public Library()
        {
            books = new List<Book>();
            members = new List<Member>();
            transactions = new List<Transaction>();
            LoadData();
        }

        public void AddBook(Book book)
        {
            if (books.Any(b => b.ISBN == book.ISBN))
            {
                Console.WriteLine("Book with this ISBN already exists!");
                return;
            }
            books.Add(book);
            Console.WriteLine($"Book '{book.Title}' added successfully!");
        }

        public Book FindBook(string isbn)
        {
            return books.FirstOrDefault(b => b.ISBN == isbn);
        }

        public List<Book> SearchBooks(string searchTerm)
        {
            return books.Where(b => b.MatchesSearch(searchTerm)).ToList();
        }

        public void RegisterMember(Member member)
        {
            if (members.Any(m => m.Id == member.Id))
            {
                Console.WriteLine("Member with this ID already exists!");
                return;
            }
            members.Add(member);
            Console.WriteLine($"Member '{member.Name}' registered successfully!");
        }

        public Member FindMember(string memberId)
        {
            return members.FirstOrDefault(m => m.Id == memberId);
        }

        public bool IssueBook(string memberId, string isbn)
        {
            var member = FindMember(memberId);
            var book = FindBook(isbn);

            if (member == null)
            {
                Console.WriteLine("Member not found!");
                return false;
            }

            if (book == null)
            {
                Console.WriteLine("Book not found!");
                return false;
            }

            if (!book.CanBorrow(member))
            {
                Console.WriteLine("Book cannot be borrowed. Check availability or member limits.");
                return false;
            }

            book.BorrowBook(member);
            var transaction = new Transaction(memberId, isbn, TransactionType.Issue);
            transactions.Add(transaction);
            member.BorrowedHistory.Add(transaction);

            Console.WriteLine($"Book '{book.Title}' issued to {member.Name}");
            Console.WriteLine($"Due Date: {transaction.DueDate:yyyy-MM-dd}");
            return true;
        }

        public bool ReturnBook(string memberId, string isbn)
        {
            var member = FindMember(memberId);
            var book = FindBook(isbn);

            if (member == null || book == null)
            {
                Console.WriteLine("Member or Book not found!");
                return false;
            }

            var issueTransaction = transactions.FirstOrDefault(t =>
                t.MemberId == memberId &&
                t.BookISBN == isbn &&
                t.Type == TransactionType.Issue &&
                t.ReturnDate == null);

            if (issueTransaction == null)
            {
                Console.WriteLine("No active borrowing record found!");
                return false;
            }

            issueTransaction.ReturnDate = DateTime.Now;
            issueTransaction.FineAmount = issueTransaction.CalculateFine();

            if (issueTransaction.FineAmount > 0)
            {
                member.FineAmount += issueTransaction.FineAmount;
                Console.WriteLine($"Fine of ${issueTransaction.FineAmount:F2} applied for late return.");
            }

            book.ReturnBook();
            var returnTransaction = new Transaction(memberId, isbn, TransactionType.Return);
            transactions.Add(returnTransaction);

            Console.WriteLine($"Book '{book.Title}' returned successfully!");
            return true;
        }

        public void DisplayAllBooks()
        {
            Console.WriteLine("\n=== ALL BOOKS ===");
            foreach (var book in books)
            {
                book.DisplayInfo();
                Console.WriteLine(new string('-', 40));
            }
        }

        public void DisplayAllMembers()
        {
            Console.WriteLine("\n=== ALL MEMBERS ===");
            foreach (var member in members)
            {
                member.DisplayInfo();
                Console.WriteLine(new string('-', 40));
            }
        }

        public void DisplayOverdueBooks()
        {
            Console.WriteLine("\n=== OVERDUE BOOKS ===");
            var overdueTransactions = transactions.Where(t => t.IsOverdue()).ToList();

            foreach (var transaction in overdueTransactions)
            {
                var member = FindMember(transaction.MemberId);
                var book = FindBook(transaction.BookISBN);
                Console.WriteLine($"Member: {member?.Name} | Book: {book?.Title} | Due: {transaction.DueDate:yyyy-MM-dd} | Fine: ${transaction.CalculateFine():F2}");
            }
        }

        public void SaveData()
        {
            try
            {
                var booksJson = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(BooksFile, booksJson);

                var membersJson = JsonSerializer.Serialize(members, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(MembersFile, membersJson);

                var transactionsJson = JsonSerializer.Serialize(transactions, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(TransactionsFile, transactionsJson);

                Console.WriteLine("Data saved successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data: {ex.Message}");
            }
        }

        private void LoadData()
        {
            try
            {
                if (File.Exists(BooksFile))
                {
                    var booksJson = File.ReadAllText(BooksFile);
                    books = JsonSerializer.Deserialize<List<Book>>(booksJson) ?? new List<Book>();
                }

                if (File.Exists(MembersFile))
                {
                    var membersJson = File.ReadAllText(MembersFile);
                    members = JsonSerializer.Deserialize<List<Member>>(membersJson) ?? new List<Member>();
                }

                if (File.Exists(TransactionsFile))
                {
                    var transactionsJson = File.ReadAllText(TransactionsFile);
                    transactions = JsonSerializer.Deserialize<List<Transaction>>(transactionsJson) ?? new List<Transaction>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
            }
        }
    }
}
