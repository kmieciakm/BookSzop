using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ShopService.StoreManagement
{
    public class StoreManagementException : Exception
    {
        public StoreManagementException()
        {
        }

        public StoreManagementException(string message) : base(message)
        {
        }

        public StoreManagementException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
