using Microsoft.VisualStudio.TestTools.UnitTesting;
using G4S.Business.Validators;
using Moq;
using G4S.Entities.Pocos;
using G4S.Business.Repositories;
using System.Threading.Tasks;
using G4S.Business.Validators.WarningStrings;
using System.Collections.Generic;
using G4S.Business.Writers;
using G4S.Entities.SearchPocos;

namespace G4S.Tests
{
    [TestClass]
    public class OrderItemValidatorTest
    {
        private Mock<IReader<OrderItem>> mockReaderOrderItem;
        private Mock<IReader<ProductType>> mockReaderProductType;
        private Mock<IReader<PurchaseOrder>> mockReaderPurchaseOrder;
        private OrderItemValidator service;
        OrderItem orderItem;
        ValidationResult result;
        List<string> messages;
        ProductType productType;
        PurchaseOrder purchaseOrder;
        List<ProductType> productsToReturn;

      [TestInitialize()]
        public void Initialize()
        {
            //arrange globals for test 
            mockReaderOrderItem = new Mock<IReader<OrderItem>>();
            mockReaderProductType = new Mock<IReader<ProductType>>();
            mockReaderPurchaseOrder = new Mock<IReader<PurchaseOrder>>();
            service = new OrderItemValidator(mockReaderOrderItem.Object, mockReaderProductType.Object, mockReaderPurchaseOrder.Object);
        }
        [TestMethod]
        public async Task TestNotInitializedOrderItemValidator()
        {
            //arrange not initialized OrderItem
            orderItem = new OrderItem();
            result = await service.ValidateAsync(orderItem);
            messages = new List<string>();
            messages.Add($"{nameof(OrderItem.PurchaseOrder)}" + OrderItemValidatorWarnings.GetMandatoryField());
            messages.Add($"{nameof(OrderItem.CostCenter)}" + OrderItemValidatorWarnings.GetMandatoryField());
            messages.Add($"{nameof(OrderItem.QuantityOfProducts)}" + OrderItemValidatorWarnings.GetZeroNotAllowed());
            messages.Add($"{nameof(OrderItem.Type)}: {orderItem.Type}" + OrderItemValidatorWarnings.GetNotValidValue());
            //assert
            Assert.AreEqual(ValidationResultCode.Invalid, result.Result);
            CollectionAssert.AreEqual(messages, result.Messages);
        }
        [TestMethod]
        public async Task TestNullAsOrderItemOrderItemValidator()
        {
            //arrange null as OrderItem
            orderItem = null;
            result = await service.ValidateAsync(orderItem);
            messages = new List<string>();
            messages.Add($"{nameof(OrderItem)}" + OrderItemValidatorWarnings.GetNullAsObject());
            //assert
            Assert.AreEqual(ValidationResultCode.Invalid, result.Result);
            CollectionAssert.AreEqual(messages, result.Messages);
        }
        [TestMethod]
        public async Task TestNullAsPurchaseOrderOrderItemValidator()
        {
            //arrange null as PurchaseOrder
            orderItem = new OrderItem();
            purchaseOrder = null;
            result = await service.ValidateAsync(orderItem);
            messages = new List<string>();
            messages.Add($"{nameof(OrderItem.PurchaseOrder)}" + OrderItemValidatorWarnings.GetMandatoryField());
            messages.Add($"{nameof(OrderItem.CostCenter)}" + OrderItemValidatorWarnings.GetMandatoryField());
            messages.Add($"{nameof(OrderItem.QuantityOfProducts)}" + OrderItemValidatorWarnings.GetZeroNotAllowed());
            messages.Add($"{nameof(OrderItem.Type)}: {orderItem.Type}" + OrderItemValidatorWarnings.GetNotValidValue());
            //assert
            Assert.AreEqual(ValidationResultCode.Invalid, result.Result);
            CollectionAssert.AreEqual(messages, result.Messages);
        }
        [TestMethod]
        public async Task TestCostcenterEmptyValueOrderItemValidator()
        {
            //arrange costcenter empty value
            orderItem = new OrderItem();
            orderItem.CostCenter = "";
            result = await service.ValidateAsync(orderItem);
            messages = new List<string>();
            messages.Add($"{nameof(OrderItem.PurchaseOrder)}" + OrderItemValidatorWarnings.GetMandatoryField());
            messages.Add($"{nameof(OrderItem.CostCenter)}" + OrderItemValidatorWarnings.GetMandatoryField());
            messages.Add($"{nameof(OrderItem.QuantityOfProducts)}" + OrderItemValidatorWarnings.GetZeroNotAllowed());
            messages.Add($"{nameof(OrderItem.Type)}: {orderItem.Type}" + OrderItemValidatorWarnings.GetNotValidValue());
            //assert
            Assert.AreEqual(ValidationResultCode.Invalid, result.Result);
            CollectionAssert.AreEqual(messages, result.Messages);
        }
        [TestMethod]
        public async Task TestCostcenterNumericValueOrderItemValidator()
        {
            //arrange costcenter numeric value
            orderItem = new OrderItem();
            orderItem.CostCenter = "645";
            result = await service.ValidateAsync(orderItem);
            messages = new List<string>();
            messages.Add($"{nameof(OrderItem.PurchaseOrder)}" + OrderItemValidatorWarnings.GetMandatoryField());
            messages.Add($"{nameof(OrderItem.QuantityOfProducts)}" + OrderItemValidatorWarnings.GetZeroNotAllowed());
            messages.Add($"{nameof(OrderItem.Type)}: {orderItem.Type}" + OrderItemValidatorWarnings.GetNotValidValue());
            //assert
            Assert.AreEqual(ValidationResultCode.Invalid, result.Result);
            CollectionAssert.AreEqual(messages, result.Messages);
        }
        [TestMethod]
        public async Task TestCostcenterStringValueOrderItemValidator()
        {
            //arrange costcenter string value
            orderItem = new OrderItem();
            orderItem.CostCenter = "dg89rqf";
            result = await service.ValidateAsync(orderItem);
            messages = new List<string>();
            messages.Add($"{nameof(OrderItem.PurchaseOrder)}" + OrderItemValidatorWarnings.GetMandatoryField());
            messages.Add($"{nameof(OrderItem.QuantityOfProducts)}" + OrderItemValidatorWarnings.GetZeroNotAllowed());
            messages.Add($"{nameof(OrderItem.Type)}: {orderItem.Type}" + OrderItemValidatorWarnings.GetNotValidValue());
            //assert
            Assert.AreEqual(ValidationResultCode.Invalid, result.Result);
            CollectionAssert.AreEqual(messages, result.Messages);
        }
        [TestMethod]
        public async Task TestQuantityOfProductsNegativeValueOrderItemValidator()
        {
            //arrange QuantityOfProducts negative value
            orderItem = new OrderItem();
            orderItem.QuantityOfProducts = -1;
            result = await service.ValidateAsync(orderItem);
            messages = new List<string>();
            messages.Add($"{nameof(OrderItem.PurchaseOrder)}" + OrderItemValidatorWarnings.GetMandatoryField());
            messages.Add($"{nameof(OrderItem.CostCenter)}" + OrderItemValidatorWarnings.GetMandatoryField());
            messages.Add($"{nameof(OrderItem.QuantityOfProducts)}" + OrderItemValidatorWarnings.GetNegativeNotAllowed());
            messages.Add($"{nameof(OrderItem.Type)}: {orderItem.Type}" + OrderItemValidatorWarnings.GetNotValidValue());
            //assert
            Assert.AreEqual(ValidationResultCode.Invalid, result.Result);
            CollectionAssert.AreEqual(messages, result.Messages);
        }
        [TestMethod]
        public async Task TestQuantityOfProductsValidValueOrderItemValidator()
        {
            //arrange QuantityOfProducts valid value
            orderItem = new OrderItem();
            orderItem.QuantityOfProducts = 1;
            result = await service.ValidateAsync(orderItem);
            messages = new List<string>();
            messages.Add($"{nameof(OrderItem.PurchaseOrder)}" + OrderItemValidatorWarnings.GetMandatoryField());
            messages.Add($"{nameof(OrderItem.CostCenter)}" + OrderItemValidatorWarnings.GetMandatoryField());
            messages.Add($"{nameof(OrderItem.Type)}: {orderItem.Type}" + OrderItemValidatorWarnings.GetNotValidValue());
            //assert
            Assert.AreEqual(ValidationResultCode.Invalid, result.Result);
            CollectionAssert.AreEqual(messages, result.Messages);
        }
        [TestMethod]
        public async Task TestTypeEmptyOrderItemValidator()
        {
            //arrange Type empty
            orderItem = new OrderItem();
            orderItem.Type = new ProductType { TypeName = "" };
            result = await service.ValidateAsync(orderItem);
            messages = new List<string>();
            messages.Add($"{nameof(OrderItem.PurchaseOrder)}" + OrderItemValidatorWarnings.GetMandatoryField());
            messages.Add($"{nameof(OrderItem.CostCenter)}" + OrderItemValidatorWarnings.GetMandatoryField());
            messages.Add($"{nameof(OrderItem.QuantityOfProducts)}" + OrderItemValidatorWarnings.GetZeroNotAllowed());
            messages.Add($"{nameof(OrderItem.Type)}: {orderItem.Type}" + OrderItemValidatorWarnings.GetNotValidValue());
            //assert
            Assert.AreEqual(ValidationResultCode.Invalid, result.Result);
            CollectionAssert.AreEqual(messages, result.Messages);
        }
        [TestMethod]
        public async Task TestTypeNullOrderItemValidator()
        {
            //TODO test type of orderItem
            //arrange Type null
            orderItem = new OrderItem();
            orderItem.Type = new ProductType { TypeName = null };
            result = await service.ValidateAsync(orderItem);
            messages = new List<string>();
            messages.Add($"{nameof(OrderItem.PurchaseOrder)}" + OrderItemValidatorWarnings.GetMandatoryField());
            messages.Add($"{nameof(OrderItem.CostCenter)}" + OrderItemValidatorWarnings.GetMandatoryField());
            messages.Add($"{nameof(OrderItem.QuantityOfProducts)}" + OrderItemValidatorWarnings.GetZeroNotAllowed());
            messages.Add($"{nameof(OrderItem.Type)}: {orderItem.Type}" + OrderItemValidatorWarnings.GetNotValidValue());
            //assert
            Assert.AreEqual(ValidationResultCode.Invalid, result.Result);
            CollectionAssert.AreEqual(messages, result.Messages);
        }
        [TestMethod]
        public async Task TestTypeValidOrderItemValidator()
        {
            //arrange Type valid value
            orderItem = new OrderItem();
            productType = new ProductType { TypeName = "Test" };
            orderItem.Type = productType;
            productsToReturn = new List<ProductType>();
            productsToReturn.Add(productType);
            mockReaderProductType.Setup(mrpt => mrpt.GetAllAsync()).ReturnsAsync(productsToReturn as IEnumerable<ProductType>);
            result = await service.ValidateAsync(orderItem);
            messages = new List<string>();
            messages.Add($"{nameof(OrderItem.PurchaseOrder)}" + OrderItemValidatorWarnings.GetMandatoryField());
            messages.Add($"{nameof(OrderItem.CostCenter)}" + OrderItemValidatorWarnings.GetMandatoryField());
            messages.Add($"{nameof(OrderItem.QuantityOfProducts)}" + OrderItemValidatorWarnings.GetZeroNotAllowed());
            //assert
            Assert.AreEqual(ValidationResultCode.Invalid, result.Result);
            CollectionAssert.AreEqual(messages, result.Messages);
        }
        [TestMethod]
        public async Task TestValidOrderItemValidator()
        {
            PurchaseOrder purchaseOrder = new PurchaseOrder() { PurchaseOrderNumber = 1 };
            productType = new ProductType { TypeName = "Test" };
            orderItem = new OrderItem
            {
                PurchaseOrder = purchaseOrder,
                CostCenter = "test",
                QuantityOfProducts = 1,
                Type = productType
            };
            productsToReturn = new List<ProductType>();
            productsToReturn.Add(productType);
            mockReaderProductType.Setup(mrpt => mrpt.GetAllAsync()).ReturnsAsync(productsToReturn as IEnumerable<ProductType>);
            mockReaderPurchaseOrder.Setup(r => r.SearchCount(It.Is<PurchaseOrderSearchCriteria>(s => s.PurchaseOrderNumber == 1))).ReturnsAsync(1);
            result = await service.ValidateAsync(orderItem);
            messages = new List<string>();
            //assert
            Assert.AreEqual(ValidationResultCode.Valid, result.Result);
            CollectionAssert.AreEqual(messages, result.Messages);
        }

        //TODO Test ValidateDeleteAsync
        //TODO TestValidateRestoreAsync
        //TODO TestValidateUpdateAsync
    }
}
