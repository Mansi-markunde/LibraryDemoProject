using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day4
{
  public class BookNotFoundException : Exception
{
    public BookNotFoundException(string message) : base(message) { }
}


}
