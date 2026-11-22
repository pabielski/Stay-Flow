using System;
using System.Collections.Generic;
using System.Text;

namespace StayFlow.Services.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
