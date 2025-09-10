using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    internal class Transaction
    {
        public string TransactionId { get; set; }
        public string MemberId { get; set; }
        public string BookISBN { get; set; }
        public TransactionType Type { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal FineAmount { get; set; }

        public Transaction(string memberId, string bookISBN, TransactionType type)
        {
            TransactionId = Guid.NewGuid().ToString();
            MemberId = memberId;
            BookISBN = bookISBN;
            Type = type;
            IssuedDate = DateTime.Now;
            DueDate = Type == TransactionType.Issue ? DateTime.Now.AddDays(14) : null;
            FineAmount = 0;
        }

        public bool IsOverdue()
        {
            return DueDate.HasValue && DateTime.Now > DueDate.Value && ReturnDate == null;
        }

        public decimal CalculateFine()
        {
            if (IsOverdue()) {
                int overdueDays = (DateTime.Now - DueDate.Value).Days;
                return overdueDays * 0.50m;
            }

            return 0;
        }
    }
}
