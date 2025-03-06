using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 namespace Day4
{

class Program
{
    static async Task Main(string[] args)
    {
        Library library = new Library();

        // Subscribe to events
        library.BookBorrowed += OnBookBorrowed;
        library.BookReturned += OnBookReturned;
        library.StockLow += OnStockLow;

        // Adding authors
        Author author1 = new Author("Author One", "author1@example.com");
        Author author2 = new Author("Author Two", "author2@example.com");

        // Adding books
        Book book1 = new Book("Book One", author1, "ISBN001", 2001);
        Book book2 = new Book("Book Two", author2, "ISBN002", 2002);
        library.AddBook(book1);
        library.AddBook(book2);

        // Adding members
        LibraryMember member1 = new RegularMember("Member One", 1);
        LibraryMember member2 = new PremiumMember("Member Two", 2);
        library.AddMember(member1);
        library.AddMember(member2);

        // Display all books
        Console.WriteLine("All Books:");
        library.DisplayAllBooks();

        // Display all members
        Console.WriteLine("\nAll Members:");
        library.DisplayAllMembers();

        // Borrow a book
        await library.BorrowBookAsync(member1, book1);
        Console.WriteLine($"\n{member1.Name} borrowed {book1.Title}");

        // Return a book
        await library.ReturnBookAsync(member1, book1);
        Console.WriteLine($"\n{member1.Name} returned {book1.Title}");

        // LINQ Queries
        var booksAscending = library.Books.OrderBy(b => b.PublicationYear).ToList();
        var booksDescending = library.Books.OrderByDescending(b => b.PublicationYear).ToList();
        var booksByAuthor = library.Books.GroupBy(b => b.Author.Name)
                                         .Select(g => new { Author = g.Key, Count = g.Count() })
                                         .ToList();
        var keyword = "Book";
        var booksWithKeyword = library.Books.Where(b => b.Title.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        var membersByType = library.LibraryMembers.GroupBy(m => m.GetType().Name)
                                                  .Select(g => new { MembershipType = g.Key, Members = g.ToList() })
                                                  .ToList();
        var booksBorrowedByMember = library.LibraryMembers.Select(m => new { Member = m.Name, BorrowedBooksCount = m.BorrowedBooks.Count }).ToList();

        // Display LINQ results
        Console.WriteLine("\nBooks sorted by publication date (ascending):");
        foreach (var book in booksAscending)
        {
            Console.WriteLine($"{book.Title} ({book.PublicationYear})");
        }

        Console.WriteLine("\nBooks sorted by publication date (descending):");
        foreach (var book in booksDescending)
        {
            Console.WriteLine($"{book.Title} ({book.PublicationYear})");
        }

        Console.WriteLine("\nBooks grouped by author:");
        foreach (var group in booksByAuthor)
        {
            Console.WriteLine($"{group.Author}: {group.Count} book(s)");
        }

        Console.WriteLine("\nBooks with keyword 'Book':");
        foreach (var book in booksWithKeyword)
        {
            Console.WriteLine($"{book.Title}");
        }

        Console.WriteLine("\nMembers grouped by membership type:");
        foreach (var group in membersByType)
        {
            Console.WriteLine($"{group.MembershipType}: {group.Members.Count} member(s)");
        }

        Console.WriteLine("\nTotal number of books borrowed by each member:");
        foreach (var member in booksBorrowedByMember)
        {
            Console.WriteLine($"{member.Member}: {member.BorrowedBooksCount} book(s)");
        }
    }

    static void OnBookBorrowed(object sender, BookEventArgs e)
    {
        Console.WriteLine($"Event: Book borrowed - {e.Book.Title}");
    }

    static void OnBookReturned(object sender, BookEventArgs e)
    {
        Console.WriteLine($"Event: Book returned - {e.Book.Title}");
    }

    static void OnStockLow(object sender, EventArgs e)
    {
        Console.WriteLine("Event: Library stock is running low!");
    }
}
}