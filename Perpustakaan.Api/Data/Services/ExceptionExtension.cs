using System;
using System.Collections;

namespace Perpustakaan.Api.Data.Services
{
    public class ExceptionExtension : Exception
    {
        private Exception _currentEx;
        private string _message;
        public ExceptionExtension(string message, Exception e) : base(message, e)
        {
            Exception currentEx = e;
            while (currentEx.InnerException != null)
            {
                currentEx = currentEx.InnerException;
            }

            _currentEx = currentEx;
            _message = message;
        }

        public override string Message => $"{_message}. error detail : {_currentEx.Message}";

        public override string StackTrace => _currentEx.StackTrace;

        public override string Source { get => _currentEx.Source; set => _currentEx.Source = value; }

        public override string HelpLink { get => _currentEx.HelpLink; set => _currentEx.HelpLink = value; }

        public override IDictionary Data => _currentEx.Data;
    }
}