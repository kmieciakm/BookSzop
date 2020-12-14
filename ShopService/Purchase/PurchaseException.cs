using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ShopService.Purchase
{
    public class PurchaseException : Exception
    {
        public PurchaseException()
        {
        }

        public PurchaseException(string message) : base(message)
        {
        }

        public PurchaseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
