using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTestFramework.Utilities
{
    public class RetryException: Exception
    {
        public RetryException(string message) : base(message)
        {
        }
    }
}
