using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day4
{
    public class Author
    {
     
        public string Name{get;set;}
        public string Email{get;set;}

        public Author(string name, string email)
        {
            Name=name;
            Email=email;
        }

    }
}