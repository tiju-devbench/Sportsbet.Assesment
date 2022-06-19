using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Exceptions
{
    public abstract class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
    }

    public class DomainInvalidOperationException : DomainException
    {
        public DomainInvalidOperationException(string message) : base(message) { }
    }
    public class DomainUnprocessableException : DomainException
    {
        public DomainUnprocessableException(string message) : base(message) { }
    }
    public class DomainNotFoundException : DomainException
    {
        public DomainNotFoundException(string message) : base(message) { }
    }
}
