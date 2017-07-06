using System;
using System.Collections.Generic;

namespace G4S.Business.Helpers
{
    public class EntityResult
    {
        public ResultCode Code { get; set; }
        public IEnumerable<string> ValidationMessages { get; set; }
        public Exception Exception { get; set; }
        public string ExceptionMessage { get { return Exception?.Message; } }

        public EntityResult(ResultCode result)
        {
            Code = result;
        }

        public EntityResult(ResultCode result, string[] validationMessages)
        {
            Code = result;
            ValidationMessages = validationMessages;
        }
    }

    public enum ResultCode
    {
        Failed,
        ValidationError,
        Success
    }
}