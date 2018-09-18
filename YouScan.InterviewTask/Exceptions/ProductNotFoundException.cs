using System;
using System.Text;

namespace YouScan.InterviewTask.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        private readonly string _productName;
        private readonly string _message;

        public ProductNotFoundException(string productName)
        {
            _productName = productName;
        }

        public ProductNotFoundException(string productName, string message)
        {
            _productName = productName;
            _message = message;
        }

        public override string Message 
            => GetExceptionMessage();

        private string GetExceptionMessage()
        {
            var sb = new StringBuilder($"Product named '{_productName}' not found.");

            if (string.IsNullOrEmpty(_message))
                sb.AppendLine(_message);

            return sb.ToString();
        }
    }
}
