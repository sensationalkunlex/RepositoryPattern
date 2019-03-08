using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Exceptions
{
    public class DbEntityValidationException : ArgumentException
    {
        public DbEntityValidationException():base() { }
        public DbEntityValidationException(string message): base(message) { }

        public DbEntityValidationException(string message, Exception exception) : base(message, exception) { }
    }
}
