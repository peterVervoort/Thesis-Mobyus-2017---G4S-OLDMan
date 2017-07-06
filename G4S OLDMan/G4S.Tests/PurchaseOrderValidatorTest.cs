using Microsoft.VisualStudio.TestTools.UnitTesting;
using G4S.Business.Validators;
using Moq;
using G4S.Business.Repositories;
using G4S.Entities.Pocos;
using System.Threading.Tasks;
using System.Collections.Generic;
using G4S.Business.Validators.WarningStrings;
using G4S.Business.Writers;
using G4S.Entities.SearchPocos;

namespace G4S.Tests
{
    [TestClass]
    public class PurchaseOrderValidatorTest
    {
        private Mock<IReader<PurchaseOrder>> mockReaderPurchaseOrder;
        private PurchaseOrderValidator serviceValidator;
        private ValidationResult result;
        private List<string> messages;
        private PurchaseOrder purchaseOrder;

        [TestInitialize()]
        public void Initialize()
        {
            //arrange globals for test 
            mockReaderPurchaseOrder = new Mock<IReader<PurchaseOrder>>();
            serviceValidator = new PurchaseOrderValidator(mockReaderPurchaseOrder.Object);
        }

        //ValidateAsync
        [TestMethod]
        public async Task TestNullAsValidateAsync()
        {
            //arrange null as PurchaseOrder
            purchaseOrder = null;
            result = await serviceValidator.ValidateAsync(purchaseOrder);
            messages = new List<string>();
            messages.Add(($"{nameof(PurchaseOrder)}" + PurchaseOrderValidatorWarnings.GetNullAsObject()));
            //assert
            Assert.AreEqual(ValidationResultCode.Invalid, result.Result);
            CollectionAssert.AreEqual(result.Messages, messages);
        }
        [TestMethod]
        public async Task TestNotInitializedValidateAsync()
        {
            //arrange not initialized purchaseOrder
            purchaseOrder = new PurchaseOrder();
            result = await serviceValidator.ValidateAsync(purchaseOrder);
            messages = new List<string>();
            messages.Add($"{nameof(PurchaseOrder)}" + PurchaseOrderValidatorWarnings.GetZeroNotAllowed());
            //assert
            Assert.AreEqual(ValidationResultCode.Invalid, result.Result);
            CollectionAssert.AreEqual(result.Messages, messages);
        }
        [TestMethod]
        public async Task TestPurChaseOrderDoesExistValidateAsync()
        {
            //arrange purchase order does exist
            purchaseOrder = new PurchaseOrder();
            purchaseOrder.PurchaseOrderNumber = 123;
            mockReaderPurchaseOrder.Setup(r => r.SearchCount(It.Is<PurchaseOrderSearchCriteria>(s => s.PurchaseOrderNumber == purchaseOrder.PurchaseOrderNumber &&
                s.Deleted != Entities.Enums.DeleteOption.OnlyDeleted))).ReturnsAsync(1);
            result = await serviceValidator.ValidateAsync(purchaseOrder);
            messages = new List<string>();
            messages.Add($"{nameof(PurchaseOrder.PurchaseOrderNumber)}: {purchaseOrder.PurchaseOrderNumber}" + PurchaseOrderValidatorWarnings.GetUsedNotUnique());
            //assert
            Assert.AreEqual(ValidationResultCode.Invalid, result.Result);
            CollectionAssert.AreEqual(result.Messages, messages);
        }

        [TestMethod]
        public async Task TestValidValidationAsync()
        {
            //arrange valid PO
            purchaseOrder = new PurchaseOrder() { PurchaseOrderNumber = 123 };
            mockReaderPurchaseOrder.Setup(r => r.SearchCount(It.Is<PurchaseOrderSearchCriteria>(s => s.PurchaseOrderNumber == purchaseOrder.PurchaseOrderNumber))).ReturnsAsync(0);
            result = await serviceValidator.ValidateAsync(purchaseOrder);
            messages = new List<string>();
            //assert
            Assert.AreEqual(ValidationResultCode.Valid, result.Result);
            CollectionAssert.AreEqual(result.Messages, messages);

        }

        //ValidateDeleteAsync
        [TestMethod]
        public async Task TestNullAsDeleteAsync()
        {
            //arrange null as PurchaseOrder
            purchaseOrder = null;
            result = await serviceValidator.ValidateDeleteAsync(purchaseOrder);
            messages = new List<string>();
            messages.Add($"{nameof(PurchaseOrder)}" + PurchaseOrderValidatorWarnings.GetNullAsObject());
            //assert
            Assert.AreEqual(ValidationResultCode.Invalid, result.Result);
            CollectionAssert.AreEqual(result.Messages, messages);
        }
        [TestMethod]
        public async Task TestNotInitializedDeleteAsync()
        {
            //arrange not initialized purchaseOrder
            purchaseOrder = new PurchaseOrder();
            result = await serviceValidator.ValidateDeleteAsync(purchaseOrder);
            messages = new List<string>();
            messages.Add($"{nameof(PurchaseOrder.PurchaseOrderNumber)}: {purchaseOrder.PurchaseOrderNumber}" + PurchaseOrderValidatorWarnings.GetNotFoundNoDelete());
            //assert
            Assert.AreEqual(ValidationResultCode.Invalid, result.Result);
            CollectionAssert.AreEqual(result.Messages, messages);
        }
        [TestMethod]
        public async Task TestNotFoundDeleteAsync()
        {
            purchaseOrder = new PurchaseOrder() { PurchaseOrderNumber = 123 };
            mockReaderPurchaseOrder.Setup(r => r.SearchCount(It.Is<PurchaseOrderSearchCriteria>(s => s.PurchaseOrderNumber == purchaseOrder.PurchaseOrderNumber))).ReturnsAsync(0);
            result = await serviceValidator.ValidateDeleteAsync(purchaseOrder);
            messages = new List<string>();
            messages.Add($"{nameof(PurchaseOrder.PurchaseOrderNumber)}: {purchaseOrder.PurchaseOrderNumber}" + PurchaseOrderValidatorWarnings.GetNotFoundNoDelete());
            //assert
            Assert.AreEqual(ValidationResultCode.Invalid, result.Result);
            CollectionAssert.AreEqual(result.Messages, messages);
        }
        [TestMethod]
        public async Task TestFoundDeleteAsync()
        {
            purchaseOrder = new PurchaseOrder() { PurchaseOrderNumber = 123 };
            mockReaderPurchaseOrder.Setup(r => r.SearchCount(It.Is<PurchaseOrderSearchCriteria>(s => s.PurchaseOrderNumber == purchaseOrder.PurchaseOrderNumber))).ReturnsAsync(1);
            result = await serviceValidator.ValidateDeleteAsync(purchaseOrder);
            messages = new List<string>();
            //assert
            Assert.AreEqual(ValidationResultCode.Valid, result.Result);
            CollectionAssert.AreEqual(result.Messages, messages);
        }

        //ValidateRestoreAsync
        [TestMethod]
        public async Task TestNullAsRestoreAsync()
        {
            //arrange null as PurchaseOrder
            purchaseOrder = null;
            result = await serviceValidator.ValidateRestoreAsync(purchaseOrder);
            messages = new List<string>();
            messages.Add($"{nameof(PurchaseOrder)}" + PurchaseOrderValidatorWarnings.GetNullAsObject());
            //assert
            Assert.AreEqual(ValidationResultCode.Invalid, result.Result);
            CollectionAssert.AreEqual(result.Messages, messages);
        }
        [TestMethod]
        public async Task TestNotInitializedValidateRestoreAsync()
        {
            //arrange
            purchaseOrder = new PurchaseOrder();
            mockReaderPurchaseOrder.Setup(r => r.SearchCount(It.Is<PurchaseOrderSearchCriteria>(s => s.PurchaseOrderNumber == purchaseOrder.PurchaseOrderNumber))).ReturnsAsync(0);
            result = await serviceValidator.ValidateRestoreAsync(purchaseOrder);
            messages = new List<string>();
            messages.Add($"{nameof(PurchaseOrder)}" + PurchaseOrderValidatorWarnings.GetZeroNotAllowed());
            messages.Add($"{nameof(PurchaseOrder.PurchaseOrderNumber)}: {purchaseOrder.PurchaseOrderNumber}" + PurchaseOrderValidatorWarnings.GetNotFoundNoRestore());
            //assert
            Assert.AreEqual(ValidationResultCode.Invalid, result.Result);
            CollectionAssert.AreEqual(result.Messages, messages);
        }
        [TestMethod]
        public async Task TestToUpdateNotRestoreValidateRestoreAsync()
        {
            //arrange
            purchaseOrder = new PurchaseOrder();
            purchaseOrder.PurchaseOrderNumber = 123;
            mockReaderPurchaseOrder.Setup(r => r.SearchCount(It.Is<PurchaseOrderSearchCriteria>(s => s.PurchaseOrderNumber == purchaseOrder.PurchaseOrderNumber))).ReturnsAsync(1);
            result = await serviceValidator.ValidateRestoreAsync(purchaseOrder);
            messages = new List<string>();
            messages.Add($"{nameof(PurchaseOrder.PurchaseOrderNumber)}: {purchaseOrder.PurchaseOrderNumber}" + PurchaseOrderValidatorWarnings.GetUsedNotUnique());
            messages.Add($"{nameof(PurchaseOrder.PurchaseOrderNumber)}: {purchaseOrder.PurchaseOrderNumber}" + PurchaseOrderValidatorWarnings.GetUpdateNoRestore());
            //assert
            Assert.AreEqual(ValidationResultCode.Invalid, result.Result);
            CollectionAssert.AreEqual(result.Messages, messages);
        }

        [TestMethod]
        public async Task TestValidRestoreAsync()
        {
            //arrange
            purchaseOrder = new PurchaseOrder() { PurchaseOrderNumber = 666 };
            mockReaderPurchaseOrder.Setup(r => r.SearchCount(It.Is<PurchaseOrderSearchCriteria>
                (s => s.PurchaseOrderNumber == purchaseOrder.PurchaseOrderNumber && s.Deleted == Entities.Enums.DeleteOption.OnlyDeleted))).ReturnsAsync(1);
            result = await serviceValidator.ValidateRestoreAsync(purchaseOrder);
            messages = new List<string>();
            //assert
            Assert.AreEqual(ValidationResultCode.Valid, result.Result);
            CollectionAssert.AreEqual(result.Messages, messages);
        }


        //TODO  ValidateUpdateAsync

        [TestMethod]
        public async Task TestPurChaseOrderAsNullValidateUpdateAsync()
        {
            //arrange
            purchaseOrder = null;
            result = await serviceValidator.ValidateUpdateAsync(purchaseOrder);
            messages = new List<string>();
            messages.Add($"{nameof(PurchaseOrder)}" + PurchaseOrderValidatorWarnings.GetNullAsObject());
            //assert
            Assert.AreEqual(ValidationResultCode.Invalid, result.Result);
            CollectionAssert.AreEqual(result.Messages, messages);
        }
        [TestMethod]
        public async Task TestPurChaseOrderNotInitialisedValidateUpdateAsync()
        {
            //arrange
            purchaseOrder = new PurchaseOrder();
            result = await serviceValidator.ValidateUpdateAsync(purchaseOrder);
            messages = new List<string>();
            messages.Add($"{nameof(PurchaseOrder)}" + PurchaseOrderValidatorWarnings.GetZeroNotAllowed());
            messages.Add($"{nameof(PurchaseOrder.PurchaseOrderNumber)}: {purchaseOrder.PurchaseOrderNumber}" + PurchaseOrderValidatorWarnings.GetNotFoundNoUpdate());
            //assert
            Assert.AreEqual(ValidationResultCode.Invalid, result.Result);
            CollectionAssert.AreEqual(result.Messages, messages);
        }
        [TestMethod]
        public async Task TestRestoreNotUpdateValidateUpdateAsync()
        {
            //arrange
            purchaseOrder = new PurchaseOrder();
            mockReaderPurchaseOrder.Setup(r => r.SearchCount(It.Is<PurchaseOrderSearchCriteria>
               (s => s.PurchaseOrderNumber == purchaseOrder.PurchaseOrderNumber && s.Deleted == Entities.Enums.DeleteOption.OnlyDeleted))).ReturnsAsync(1);
            result = await serviceValidator.ValidateUpdateAsync(purchaseOrder);
            messages = new List<string>();
            messages.Add($"{nameof(PurchaseOrder)}" + PurchaseOrderValidatorWarnings.GetZeroNotAllowed());
            messages.Add($"{nameof(PurchaseOrder.PurchaseOrderNumber)}: {purchaseOrder.PurchaseOrderNumber}" + PurchaseOrderValidatorWarnings.GetRestoreNotUpdate());
            //assert
            Assert.AreEqual(ValidationResultCode.Invalid, result.Result);
            CollectionAssert.AreEqual(result.Messages, messages);
        }
        [TestMethod]
        public async Task TestValidValidateUpdateAsync()
        {
            //arrange
            purchaseOrder = new PurchaseOrder();
            purchaseOrder.PurchaseOrderNumber = 123;
            mockReaderPurchaseOrder.Setup(r => r.SearchCount(It.Is<PurchaseOrderSearchCriteria>(s => s.PurchaseOrderNumber == purchaseOrder.PurchaseOrderNumber))).ReturnsAsync(-1);
            result = await serviceValidator.ValidateUpdateAsync(purchaseOrder);
            messages = new List<string>();
            //assert
            Assert.AreEqual(ValidationResultCode.Valid, result.Result);
            CollectionAssert.AreEqual(result.Messages, messages);
        }

        //TODO ValidateInsertAsync
        
        [TestMethod]
        public async Task TestPurChaseOrderAsNullValidateInsertAsync()
        {
             //arrange
            purchaseOrder = null;
            result = await serviceValidator.ValidateUpdateAsync(purchaseOrder);
            messages = new List<string>();
            messages.Add($"{nameof(PurchaseOrder)}" + PurchaseOrderValidatorWarnings.GetNullAsObject());
            //assert
            Assert.AreEqual(ValidationResultCode.Invalid, result.Result);
            CollectionAssert.AreEqual(result.Messages, messages);
        }
        [TestMethod]
        public async Task TestPurChaseOrderNotInitialisedValidateInsertAsync()
        {
            //arrange
            purchaseOrder = new PurchaseOrder();
            result = await serviceValidator.ValidateUpdateAsync(purchaseOrder);
            messages = new List<string>();
            messages.Add($"{nameof(PurchaseOrder)}" + PurchaseOrderValidatorWarnings.GetZeroNotAllowed());
            messages.Add($"{nameof(PurchaseOrder.PurchaseOrderNumber)}: {purchaseOrder.PurchaseOrderNumber}" + PurchaseOrderValidatorWarnings.GetNotFoundNoUpdate());
            //assert
            Assert.AreEqual(ValidationResultCode.Invalid, result.Result);
            CollectionAssert.AreEqual(result.Messages, messages);
        }
        [TestMethod]
        public async Task TestPurChaseOrderDoesExistInDeletedValidateInsertAsync()
        {
            //arrange purchase order does exists in deleted items
            purchaseOrder = new PurchaseOrder();
            purchaseOrder.PurchaseOrderNumber = 321;
            mockReaderPurchaseOrder.Setup(r => r.SearchCount(It.Is<PurchaseOrderSearchCriteria>(s => s.PurchaseOrderNumber == purchaseOrder.PurchaseOrderNumber &&
                s.Deleted == Entities.Enums.DeleteOption.OnlyDeleted))).ReturnsAsync(1);
            result = await serviceValidator.ValidateInsertAsync(purchaseOrder);
            messages = new List<string>();
            messages.Add($"{nameof(PurchaseOrder.PurchaseOrderNumber)}: {purchaseOrder.PurchaseOrderNumber}" + PurchaseOrderValidatorWarnings.GetRestoreNotInsert());
            //assert
            Assert.AreEqual(ValidationResultCode.Invalid, result.Result);
            CollectionAssert.AreEqual(result.Messages, messages);
        }
        [TestMethod]
        public async Task TestValidValidateInsertAsync()
        {
        //arrange
            purchaseOrder = new PurchaseOrder();
            purchaseOrder.PurchaseOrderNumber = 123;
            mockReaderPurchaseOrder.Setup(r => r.SearchCount(It.Is<PurchaseOrderSearchCriteria>(s => s.PurchaseOrderNumber == purchaseOrder.PurchaseOrderNumber))).ReturnsAsync(0);
            result = await serviceValidator.ValidateInsertAsync(purchaseOrder);
            messages = new List<string>();
            //assert
            Assert.AreEqual(ValidationResultCode.Valid, result.Result);
            CollectionAssert.AreEqual(result.Messages, messages);
        }
    }
}
