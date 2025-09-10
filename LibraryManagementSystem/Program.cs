
namespace LibraryManagementSystem
{

    public enum BookCategory
    {
        Fiction,
        Science_and_Technology,
        History,
        Biography,
    }

    public enum MemberType
    {
        Student,
        Faculty,
        GeneralPublic
    }

    public enum TransactionType
    {
        Issue,
        Return
    }

    public class Program
    {
        private static Library library = new Library();

        static void Main(string[] args)
        {
            Console.WriteLine("=== LIBRARY MANAGEMENT SYSTEM ===");

            // Add some sample data
            InitializeSampleData();

            bool running = true;
            while (running)
            {
                DisplayMenu();
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddNewBook();
                        break;
                    case "2":
                        RegisterNewMember();
                        break;
                    case "3":
                        IssueBookToMember();
                        break;
                    case "4":
                        ReturnBookFromMember();
                        break;
                    case "5":
                        SearchBooks();
                        break;
                    case "6":
                        library.DisplayAllBooks();
                        break;
                    case "7":
                        library.DisplayAllMembers();
                        break;
                    case "8":
                        library.DisplayOverdueBooks();
                        break;
                    case "9":
                        library.SaveData();
                        break;
                    case "0":
                        library.SaveData();
                        running = false;
                        Console.WriteLine("Thank you for using Library Management System!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice! Please try again.");
                        break;
                }

                if (running)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("\n=== MAIN MENU ===");
            Console.WriteLine("1. Add New Book");
            Console.WriteLine("2. Register New Member");
            Console.WriteLine("3. Issue Book");
            Console.WriteLine("4. Return Book");
            Console.WriteLine("5. Search Books");
            Console.WriteLine("6. Display All Books");
            Console.WriteLine("7. Display All Members");
            Console.WriteLine("8. Display Overdue Books");
            Console.WriteLine("9. Save Data");
            Console.WriteLine("0. Exit");
            Console.Write("Enter your choice: ");
        }

        static void AddNewBook()
        {
            Console.WriteLine("\n=== ADD NEW BOOK ===");
            Console.Write("Enter ISBN: ");
            string isbn = Console.ReadLine();

            Console.Write("Enter Title: ");
            string title = Console.ReadLine();

            Console.Write("Enter Author: ");
            string author = Console.ReadLine();

            Console.WriteLine("Select Category:");
            var categories = Enum.GetValues<BookCategory>();
            for (int i = 0; i < categories.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {categories[i]}");
            }
            Console.Write("Enter choice: ");
            if (int.TryParse(Console.ReadLine(), out int categoryChoice) &&
                categoryChoice > 0 && categoryChoice <= categories.Length)
            {
                var category = categories[categoryChoice - 1];

                Console.Write("Enter Total Copies: ");
                if (int.TryParse(Console.ReadLine(), out int copies))
                {
                    Console.Write("Enter Price: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal price))
                    {
                        Console.Write("Enter Published Date (yyyy-mm-dd): ");
                        if (DateTime.TryParse(Console.ReadLine(), out DateTime publishedDate))
                        {
                            var book = new Book(isbn, title, author, category, copies, price, publishedDate);
                            library.AddBook(book);
                        }
                        else
                        {
                            Console.WriteLine("Invalid date format!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid price!");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid number of copies!");
                }
            }
            else
            {
                Console.WriteLine("Invalid category choice!");
            }
        }

        static void RegisterNewMember()
        {
            Console.WriteLine("\n=== REGISTER NEW MEMBER ===");
            Console.Write("Enter Member ID: ");
            string id = Console.ReadLine();

            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            Console.Write("Enter Phone: ");
            string phone = Console.ReadLine();

            Console.WriteLine("Select Member Type:");
            var memberTypes = Enum.GetValues<MemberType>();
            for (int i = 0; i < memberTypes.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {memberTypes[i]}");
            }
            Console.Write("Enter choice: ");
            if (int.TryParse(Console.ReadLine(), out int typeChoice) &&
                typeChoice > 0 && typeChoice <= memberTypes.Length)
            {
                var memberType = memberTypes[typeChoice - 1];
                var member = new Member(id, name, email, phone, memberType);
                library.RegisterMember(member);
            }
            else
            {
                Console.WriteLine("Invalid member type choice!");
            }
        }

        static void IssueBookToMember()
        {
            Console.WriteLine("\n=== ISSUE BOOK ===");
            Console.Write("Enter Member ID: ");
            string memberId = Console.ReadLine();

            Console.Write("Enter Book ISBN: ");
            string isbn = Console.ReadLine();

            library.IssueBook(memberId, isbn);
        }

        static void ReturnBookFromMember()
        {
            Console.WriteLine("\n=== RETURN BOOK ===");
            Console.Write("Enter Member ID: ");
            string memberId = Console.ReadLine();

            Console.Write("Enter Book ISBN: ");
            string isbn = Console.ReadLine();

            library.ReturnBook(memberId, isbn);
        }

        static void SearchBooks()
        {
            Console.WriteLine("\n=== SEARCH BOOKS ===");
            Console.Write("Enter search term (title, author, ISBN, or category): ");
            string searchTerm = Console.ReadLine();

            var results = library.SearchBooks(searchTerm);

            if (results.Any())
            {
                Console.WriteLine($"\nFound {results.Count} book(s):");
                foreach (var book in results)
                {
                    book.DisplayInfo();
                    Console.WriteLine(new string('-', 40));
                }
            }
            else
            {
                Console.WriteLine("No books found matching your search.");
            }
        }

        static void InitializeSampleData()
        {
            // Add sample books
            library.AddBook(new Book("978-0134685991", "Effective Java", "Joshua Bloch",
                BookCategory.Science_and_Technology, 5, 45.99m, new DateTime(2017, 12, 27)));

            library.AddBook(new Book("978-0132350884", "Clean Code", "Robert C. Martin",
                BookCategory.Science_and_Technology, 3, 42.99m, new DateTime(2008, 8, 1)));

            library.AddBook(new Book("978-0451524935", "1984", "George Orwell",
                BookCategory.Fiction, 10, 13.99m, new DateTime(1949, 6, 8)));

            // Add sample members
            library.RegisterMember(new Member("S001", "John Doe", "john.doe@email.com",
                "123-456-7890", MemberType.Student));

            library.RegisterMember(new Member("F001", "Dr. Jane Smith", "jane.smith@email.com",
                "098-765-4321", MemberType.Faculty));
        }
    }
}