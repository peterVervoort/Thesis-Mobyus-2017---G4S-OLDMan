using G4S.Business.Repositories;
using G4S.Business.Validators.WarningStrings;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G4S.Business.Validators
{
    public class OrderItemValidator : ValidatorBase<OrderItem>, IValidator<OrderItem>
    {
        private readonly IReader<OrderItem> _readerOrderItem;
        private readonly IReader<ProductType> _readerProductType;
        private readonly IReader<PurchaseOrder> _readerPurchaseOrder;


        public OrderItemValidator(IReader<OrderItem> readerOrderItem, IReader<ProductType> readerProductType, IReader<PurchaseOrder> readerPurchaseOrder)
        {
            _readerOrderItem = readerOrderItem;
            _readerProductType = readerProductType;
            _readerPurchaseOrder = readerPurchaseOrder;
        }

        public override async Task<ValidationResult> ValidateAsync(OrderItem orderItem)
        {
            ValidationResult result = new ValidationResult(ValidationResultCode.Valid);

            if (orderItem == null)
            {
                result.AddMessageAndSetInvalid($"{nameof(OrderItem)}" + OrderItemValidatorWarnings.GetNullAsObject());
            }
            
            else
            {
                
                if (orderItem.PurchaseOrderId == 0)
                {
                    result.AddMessageAndSetInvalid($"{nameof(OrderItem.PurchaseOrderId)}" + OrderItemValidatorWarnings.GetNotValidValue());
                }
                if (string.IsNullOrWhiteSpace(orderItem.CostCenter))
                {
                    result.AddMessageAndSetInvalid($"{nameof(OrderItem.CostCenter)}" + OrderItemValidatorWarnings.GetMandatoryField());
                }
                if (orderItem.QuantityOfProducts == 0)
                {
                    result.AddMessageAndSetInvalid($"{nameof(OrderItem.QuantityOfProducts)}" + OrderItemValidatorWarnings.GetZeroNotAllowed());
                }
                if (orderItem.QuantityOfProducts < 0)
                {
                    result.AddMessageAndSetInvalid($"{nameof(OrderItem.QuantityOfProducts)}" + OrderItemValidatorWarnings.GetNegativeNotAllowed());
                }
                if (orderItem.TypeId == 0)
                {
                    result.AddMessageAndSetInvalid($"{nameof(OrderItem.Type)}: {orderItem.TypeId}" + OrderItemValidatorWarnings.GetNotValidValue());
                } else
                {
                    var type = await _readerProductType.GetById(orderItem.TypeId);
                    if (type.DeviceTypeRequired)
                    {
                        if (!orderItem.DeviceTypeId.HasValue)
                        {
                            result.AddMessageAndSetInvalid($"{nameof(OrderItem.DeviceType)}: {orderItem.DeviceTypeId}" + OrderItemValidatorWarnings.GetMandatoryField());
                        }
                    }
                }

            }
            return await Task.FromResult(result);
        }

        public override async Task<ValidationResult> ValidateDeleteAsync(OrderItem orderItem)
        {
            ValidationResult result = new ValidationResult(ValidationResultCode.Valid);
            if (orderItem == null)
            {
                result.AddMessageAndSetInvalid($"{nameof(OrderItem)}" + OrderItemValidatorWarnings.GetNullAsObject());
            }
            else
            {
                if (0 == await _readerOrderItem.SearchCount(new OrderItemSearchCriteria { Id = orderItem.Id }))
                {
                    result.AddMessageAndSetInvalid($"{nameof(OrderItem)}" + OrderItemValidatorWarnings.GetNotFoundNoDelete());
                }
            }
            return await Task.FromResult(result);
        }

        public override async Task<ValidationResult> ValidateRestoreAsync(OrderItem orderItem)
        {
            ValidationResult result = new ValidationResult(ValidationResultCode.Valid);
            if (orderItem == null)
            {
                result.AddMessageAndSetInvalid($"{nameof(OrderItem)}" + OrderItemValidatorWarnings.GetNullAsObject());
            }
            else
            {
                result = await ValidateAsync(orderItem);
                if (await _readerOrderItem.SearchCount(new OrderItemSearchCriteria { Id = orderItem.Id }) > 0)
                {
                    result.AddMessageAndSetInvalid($"{nameof(OrderItem.Id)}" + OrderItemValidatorWarnings.GetUpdateNoRestore());
                }
                if (await _readerOrderItem.SearchCount(new OrderItemSearchCriteria { Id = orderItem.Id, Deleted = Entities.Enums.DeleteOption.OnlyDeleted }) != 1)
                {
                    result.AddMessageAndSetInvalid($"{nameof(OrderItem.Id)}" + OrderItemValidatorWarnings.GetUpdateNoRestore());
                }
            }
            return await Task.FromResult(result);
        }

        public override async Task<ValidationResult> ValidateUpdateAsync(OrderItem orderItem)
        {
            ValidationResult result = new ValidationResult(ValidationResultCode.Valid);
            if (orderItem == null)
            {
                result.AddMessageAndSetInvalid($"{nameof(OrderItem)}" + OrderItemValidatorWarnings.GetNullAsObject());
            }
            else
            {
                result = await ValidateAsync(orderItem);
                if ( await _readerOrderItem.SearchCount(new OrderItemSearchCriteria { Id = orderItem.Id }) == 0)
                {
                    result.AddMessageAndSetInvalid($"{nameof(OrderItem)}" + OrderItemValidatorWarnings.GetNotFoundNoUpdate());
                    if (await _readerOrderItem.SearchCount(new OrderItemSearchCriteria { Id = orderItem.Id, Deleted = Entities.Enums.DeleteOption.OnlyDeleted }) == 1)
                    {
                        result.AddMessageAndSetInvalid($"{nameof(OrderItem)}" + OrderItemValidatorWarnings.GetRestoreNotUpdate());
                    }
                }
            }
            return await Task.FromResult(result);
        }

    }
}