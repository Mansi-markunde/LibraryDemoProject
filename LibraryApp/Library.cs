using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day4
{
    public class Library
    {
        public List<Book> Books { get; set; }
    public List<LibraryMember> LibraryMembers { get; set; }

    // Define delegates
    public delegate void BookBorrowedHandler(object sender, BookEventArgs e);
    public delegate void BookReturnedHandler(object sender, BookEventArgs e);
    public delegate void StockLowHandler(object sender, EventArgs e);

    // Define events
    public event BookBorrowedHandler? BookBorrowed;
    public event BookReturnedHandler? BookReturned;
    public event StockLowHandler? StockLow;

    public Library()
    {
        Books = new List<Book>();
        LibraryMembers = new List<LibraryMember>();
    }

    public void AddBook(Book book)
    {
        Books.Add(book);
    }

    public void UpdateBook(string isbn, Book updatedBook)
    {
        var book = Books.FirstOrDefault(b => b.ISBN == isbn);
        if (book != null)
        {
            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.PublicationYear = updatedBook.PublicationYear;
        }
        else
        {
            throw new BookNotFoundException("Book not found.");
        }
    }

    public void DeleteBook(string isbn)
    {
        var book = Books.FirstOrDefault(b => b.ISBN == isbn);
        if (book != null)
        {
            Books.Remove(book);
        }
        else
        {
            throw new BookNotFoundException("Book not found.");
        }
    }

    public Book SearchBook(string isbn)
    {
        var book = Books.FirstOrDefault(b => b.ISBN == isbn);
        if (book == null)
        {
            throw new BookNotFoundException("Book not found.");
        }
        return book;
    }

    public void AddMember(LibraryMember member)
    {
        LibraryMembers.Add(member);
    }

    public void UpdateMember(int memberId, LibraryMember updatedMember)
    {
        var member = LibraryMembers.FirstOrDefault(m => m.MemberId == memberId);
        if (member != null)
        {
            member.Name = updatedMember.Name;
            member.BorrowedBooks = updatedMember.BorrowedBooks;
        }
        else
        {
            throw new MemberLimitExceededException("Member not found.");
        }
    }

    public void DeleteMember(int memberId)
    {
        var member = LibraryMembers.FirstOrDefault(m => m.MemberId == memberId);
        if (member != null)
        {
            LibraryMembers.Remove(member);
        }
        else
        {
            throw new MemberLimitExceededException("Member not found.");
        }
    }

    public LibraryMember SearchMember(int memberId)
    {
        var member = LibraryMembers.FirstOrDefault(m => m.MemberId == memberId);
        if (member == null)
        {
            throw new MemberLimitExceededException("Member not found.");
        }
        return member;
    }

    public void DisplayAllBooks()
    {
        foreach (var book in Books)
        {
            Console.WriteLine($"{book.Title} by {book.Author.Name}, ISBN: {book.ISBN}, Year: {book.PublicationYear}");
        }
    }

    public void DisplayAllMembers()
    {
        foreach (var member in LibraryMembers)
        {
            Console.WriteLine($"Member: {member.Name}, ID: {member.MemberId}");
        }
    }

    public async Task BorrowBookAsync(LibraryMember member, Book book)
    {
        await Task.Run(() =>
        {
            member.BorrowedBooks.Add(book);
            Books.Remove(book);
            OnBookBorrowed(new BookEventArgs(book));
            CheckStock();
        });
    }

    public async Task ReturnBookAsync(LibraryMember member, Book book)
    {
        await Task.Run(() =>
        {
            member.BorrowedBooks.Remove(book);
            Books.Add(book);
            OnBookReturned(new BookEventArgs(book));
        });
    }

    protected virtual void OnBookBorrowed(BookEventArgs e)
    {
        BookBorrowed?.Invoke(this, e);
    }

    protected virtual void OnBookReturned(BookEventArgs e)
    {
        BookReturned?.Invoke(this, e);
    }

    protected virtual void OnStockLow(EventArgs e)
    {
        StockLow?.Invoke(this, e);
    }

    private void CheckStock()
    {
        if (Books.Count < 5) // Assuming stock is low if less than 5 books are available
        {
            OnStockLow(EventArgs.Empty);
        }
    }
}
}
