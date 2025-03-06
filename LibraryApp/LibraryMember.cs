using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day4
{
    public class LibraryMember
    {
        public string Name{get;set;}
        public int MemberId{get;set;}
       public List<Book>BorrowedBooks{get;set;}

       public LibraryMember(string name,int memberId)
       {
        Name=name;
        MemberId=memberId;
        BorrowedBooks=new List<Book>();
       }

    }
}