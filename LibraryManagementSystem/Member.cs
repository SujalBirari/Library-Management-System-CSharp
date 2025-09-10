using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    internal class Member: Person
    {
        public MemberType Type { get; set; }
        public DateTime MembershipDate { get; set; }
        public List<Transaction> BorrowedHistory { get; set; }
        public int MaxBooksAllowed { get; set; }
        public decimal FineAmount { get; set; }

        public Member(string id, string name, string email, string phone, MemberType type):
            base(id, name, email, phone)
        {
            Type = type;
            MembershipDate = DateTime.Now;
            BorrowedHistory = new List<Transaction>();
            FineAmount = 0;
            SetMaxBooksAllowed();
        }

        private void SetMaxBooksAllowed()
        {
            MaxBooksAllowed = Type switch
            {
                MemberType.Student => 3,
                MemberType.Faculty => 10,
                MemberType.GeneralPublic => 2,
                _ => 2
            };
        }

        public bool CanBorrowMoreBooks()
        {
            int currentlyBorrowed = BorrowedHistory.Count(t => t.Type == TransactionType.Issue && t.ReturnDate == null);
            return currentlyBorrowed < MaxBooksAllowed && FineAmount == 0;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Member ID: {Id}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Type: {Type}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Phone: {Phone}");
            Console.WriteLine($"Membership Date: {MembershipDate:yyyy-MM-dd}");
            Console.WriteLine($"Max Books Allowed: {MaxBooksAllowed}");
            Console.WriteLine($"Fine Amount: ${FineAmount:F2}");
        }

        public override string GetFullDetails()
        {
            return base.GetFullDetails() + $", Type: {Type}, Fine: ${FineAmount:F2}";
        }
    }
}
