using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealexPayments
{
    public class DataValidationException : ApplicationException
    {
        public DataValidationException(string message)
            : base(message)
        {
        }

        public DataValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class ReadOnlyException : ApplicationException
    {
        public ReadOnlyException(string message)
            : base(message)
        {
        }

        public ReadOnlyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class TransactionFailedException : ApplicationException
    {
        public TransactionFailedException(string message)
            : base(message)
        {
        }

        public TransactionFailedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
