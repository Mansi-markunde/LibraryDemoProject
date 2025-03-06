using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day4
{
    public class Book
    {
     public string Title{get;set;}
     public Author Author{get;set;}
     public string ISBN{get;set;}   
     public int  PublicationYear{get;set;}

     public Book(string title,Author author , string isbn,int publicationYear)
     {
        Title=title;
        Author=author;
        ISBN=isbn;
        PublicationYear=publicationYear;
     }
    }
}