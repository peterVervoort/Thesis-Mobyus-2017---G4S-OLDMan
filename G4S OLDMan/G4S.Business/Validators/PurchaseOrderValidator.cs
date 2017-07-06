using G4S.Business.Repositories;
using G4S.Business.Validators.WarningStrings;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace G4S.Business.Validators
{
    public class PurchaseOrderValidator : ValidatorBase<PurchaseOrder>, IValidator<PurchaseOrder>
    {
        private readonly IReader<PurchaseOrder> _reader;

        public PurchaseOrderValidator(IReader<PurchaseOrder> reader)
        {
            _reader = reader;
        }

        public override async Task<ValidationResult> ValidateAsync(PurchaseOrder purchaseOrder)
        {
            ValidationResult result = new ValidationResult(ValidationResultCode.Valid);

            if (purchaseOrder == null)
            {
                result.AddMessageAndSetInvalid($"{nameof(PurchaseOrder)}" + PurchaseOrderValidatorWarnings.GetNullAsObject());
            }
            else
            {
                if (string.IsNullOrWhiteSpace(purchaseOrder.PurchaseOrderNumber.ToString()))
                {
                    result.AddMessageAndSetInvalid($"{nameof(PurchaseOrder.PurchaseOrderNumber)}" + PurchaseOrderValidatorWarnings.GetMandatoryField());
                }
                if (purchaseOrder.PurchaseOrderNumber == 0)
                {
                    result.AddMessageAndSetInvalid($"{nameof(PurchaseOrder)}" + PurchaseOrderValidatorWarnings.GetZeroNotAllowed());
                }

            }
            return result;
        }

        public override async Task<ValidationResult> ValidateInsertAsync(PurchaseOrder purchaseOrder)
        {
            ValidationResult result = new ValidationResult(ValidationResultCode.Valid);
            if (purchaseOrder == null)
            {
                result.AddMessageAndSetInvalid($"{nameof(PurchaseOrder)}" + PurchaseOrderValidatorWarnings.GetNullAsObject());
            }
            else
            {
                result = await ValidateAsync(purchaseOrder);

                int purchaseOrderCount = await _reader.SearchCount(new PurchaseOrderSearchCriteria { PurchaseOrderNumber = purchaseOrder.PurchaseOrderNumber });
                if (purchaseOrderCount > 0)
                {
                    result.AddMessageAndSetInvalid($"{nameof(PurchaseOrder.PurchaseOrderNumber)}: {purchaseOrder.PurchaseOrderNumber}" + PurchaseOrderValidatorWarnings.GetUsedNotUnique());
                }

                int purchaseOrderDeletedCount = await _reader.SearchCount(new PurchaseOrderSearchCriteria
                {
                    PurchaseOrderNumber = purchaseOrder.PurchaseOrderNumber,
                    Deleted = Entities.Enums.DeleteOption.OnlyDeleted
                });
                if (purchaseOrderDeletedCount > 0)
                {
                    result.AddMessageAndSetInvalid($"{nameof(PurchaseOrder.PurchaseOrderNumber)}: {purchaseOrder.PurchaseOrderNumber}" + PurchaseOrderValidatorWarnings.GetRestoreNotInsert());
                }
            }
            return result;
        }

        public override async Task<ValidationResult> ValidateDeleteAsync(PurchaseOrder purchaseOrder)
        {
            ValidationResult result = new ValidationResult(ValidationResultCode.Valid);

            if (purchaseOrder == null)
            {
                result.AddMessageAndSetInvalid($"{nameof(PurchaseOrder)}" + PurchaseOrderValidatorWarnings.GetNullAsObject());
            }
            else
            {

                int purchaseOrderCount = await _reader.SearchCount(new PurchaseOrderSearchCriteria { PurchaseOrderNumber = purchaseOrder.PurchaseOrderNumber });
                if (purchaseOrderCount == 0)
                {
                    result.AddMessageAndSetInvalid($"{nameof(PurchaseOrder.PurchaseOrderNumber)}: {purchaseOrder.PurchaseOrderNumber}" + PurchaseOrderValidatorWarnings.GetNotFoundNoDelete());
                }
            }

            return result;
        }

        public override async Task<ValidationResult> ValidateRestoreAsync(PurchaseOrder purchaseOrder)
        {
            ValidationResult result = new ValidationResult(ValidationResultCode.Valid);
            if (purchaseOrder == null)
            {
                result.AddMessageAndSetInvalid($"{nameof(PurchaseOrder)}" + PurchaseOrderValidatorWarnings.GetNullAsObject());
            }
            else
            {
                result = await ValidateAsync(purchaseOrder);

                int purchaseOrderCount = await _reader.SearchCount(new PurchaseOrderSearchCriteria { PurchaseOrderNumber = purchaseOrder.PurchaseOrderNumber });
                if (purchaseOrderCount > 0)
                {
                    result.AddMessageAndSetInvalid($"{nameof(PurchaseOrder.PurchaseOrderNumber)}: {purchaseOrder.PurchaseOrderNumber}" + PurchaseOrderValidatorWarnings.GetUpdateNoRestore());
                }
                else
                {
                    if (result.Messages.Contains($"{nameof(PurchaseOrder.PurchaseOrderNumber)}: {purchaseOrder.PurchaseOrderNumber}" + PurchaseOrderValidatorWarnings.GetUsedNotUnique()) || purchaseOrder.PurchaseOrderNumber == 0)
                    {
                        result.Messages.RemoveAll(m => m == $"{nameof(PurchaseOrder.PurchaseOrderNumber)}: {purchaseOrder.PurchaseOrderNumber}" + PurchaseOrderValidatorWarnings.GetUsedNotUnique());
                        int purchaseOrderDeletedCount = await _reader.SearchCount(new PurchaseOrderSearchCriteria { PurchaseOrderNumber = purchaseOrder.PurchaseOrderNumber, Deleted = Entities.Enums.DeleteOption.OnlyDeleted });
                        if (purchaseOrderDeletedCount != 1)
                        {
                            result.AddMessageAndSetInvalid($"{nameof(PurchaseOrder.PurchaseOrderNumber)}: {purchaseOrder.PurchaseOrderNumber}" + PurchaseOrderValidatorWarnings.GetNotFoundNoRestore());
                        }
                    }
                }

            }

            return result;
        }

        public override async Task<ValidationResult> ValidateUpdateAsync(PurchaseOrder purchaseOrder)
        {
            ValidationResult result = new ValidationResult(ValidationResultCode.Valid);
            if (purchaseOrder == null)
            {
                result.AddMessageAndSetInvalid($"{nameof(PurchaseOrder)}" + PurchaseOrderValidatorWarnings.GetNullAsObject());
            }
            else
            {
                result = await ValidateAsync(purchaseOrder);

                int purchaseOrderDeletedCount = await _reader.SearchCount(new PurchaseOrderSearchCriteria { PurchaseOrderNumber = purchaseOrder.PurchaseOrderNumber, Deleted = Entities.Enums.DeleteOption.OnlyDeleted });
                int purchaseOrderCount = await _reader.SearchCount(po => po.PurchaseOrderNumber == purchaseOrder.PurchaseOrderNumber);
                if (purchaseOrderDeletedCount == 1)
                {
                    result.AddMessageAndSetInvalid($"{nameof(PurchaseOrder.PurchaseOrderNumber)}: {purchaseOrder.PurchaseOrderNumber}" + PurchaseOrderValidatorWarnings.GetRestoreNotUpdate());
                }
                else if (purchaseOrderCount > 1)
                {
                    result.AddMessageAndSetInvalid($"{nameof(PurchaseOrder.PurchaseOrderNumber)}: {purchaseOrder.PurchaseOrderNumber}" + PurchaseOrderValidatorWarnings.GetUsedNotUnique());
                }
                else if (purchaseOrderCount == 1)
                {
                    var poResult = await _reader.Search(po => po.PurchaseOrderNumber == purchaseOrder.PurchaseOrderNumber);
                    if (poResult.FirstOrDefault() != null && poResult.FirstOrDefault().Id != purchaseOrder.Id)
                    {
                        result.AddMessageAndSetInvalid($"{nameof(PurchaseOrder.PurchaseOrderNumber)}: {purchaseOrder.PurchaseOrderNumber}" + PurchaseOrderValidatorWarnings.GetUsedNotUnique());
                    }
                }
            }
            return result;
        }
    }
}

