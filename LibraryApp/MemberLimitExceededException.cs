using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day4
{
   public class MemberLimitExceededException : Exception
{
    public MemberLimitExceededException(string message) : base(message) { }
}
}