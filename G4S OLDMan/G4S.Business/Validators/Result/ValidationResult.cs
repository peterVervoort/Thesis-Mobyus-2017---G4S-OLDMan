using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G4S.Business.Validators
{
    public class ValidationResult
    {
        public ValidationResult(ValidationResultCode result) : this()
        {
            this.Result = result;
        }

        public ValidationResult()
        {
            Messages = new List<string>();
        }

        public void AddMessageAndSetInvalid(string message)
        {
            this.Messages.Add(message);
            this.Result = ValidationResultCode.Invalid;
        }

        public void JoinOtherResult(ValidationResult result)
        {
            if (result.Result == ValidationResultCode.Invalid)
            {
                this.Result = ValidationResultCode.Invalid;
            }
            foreach (string message in result.Messages)
            {
                this.Messages.Add(message);
            }
        }

        public List<string> Messages { get; set; }
        public ValidationResultCode Result { get; set; }
    }

    public enum ValidationResultCode
    {
        Valid,
        //Warnings,
        Invalid
    }
}
