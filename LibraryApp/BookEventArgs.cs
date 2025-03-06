using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day4
{
   public class BookEventArgs : EventArgs
{
    public Book Book { get; }

    public BookEventArgs(Book book)
    {
        Book = book;
    }
}
}