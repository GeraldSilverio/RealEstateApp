using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace RealEstateApp.Core.Application.Exceptions
{
    public class ApiException : Exception
    {
        public int ErrorCode { get; set; }
        public ApiException() : base() { }

        public ApiException(string message) : base(message) {        
        }

        public ApiException(string message,int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public ApiException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
