using System;
using System.Runtime.Serialization;

namespace G4S.DataAccess.Repositories
{
    [Serializable]
    internal class HistoryException : Exception
    {
        public HistoryException()
        {
        }

        public HistoryException(string message) : base(message)
        {
        }

        public HistoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected HistoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}