using System;
using System.Runtime.Serialization;

namespace SimulationTradeReader
{
    [Serializable]
    internal class AccountBalanceIsNegativeException : Exception
    {
        public AccountBalanceIsNegativeException()
        {
        }

        public AccountBalanceIsNegativeException(string message) : base(message)
        {
        }

        public AccountBalanceIsNegativeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountBalanceIsNegativeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}