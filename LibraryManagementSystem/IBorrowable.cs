using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    internal interface IBorrowable
    {
        bool CanBorrow(Member member);
        void BorrowBook(Member member);
        void ReturnBook();
    }
}
