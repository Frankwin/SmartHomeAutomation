using System;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartHomeAutomation.Domain.Models.DeviceModels;

namespace SmartHomeAutomation.Services.Tests.Device
{
    [TestClass]
    public class ManufacturerServiceTests : TestBase
    {
        [TestMethod]        
        public void CreateNewManufacturerWithUpsertTest()
        {
            var newManufacturer = new Manufacturer { ManufacturerName = "New Upsert Test Manufacturer"};
            ManufacturerService.Upsert(newManufacturer, TestUserPrincipal);
            var foundManufacturers = ManufacturerService.Search("New Upsert Test Manufacturer").ToList();

            Assert.AreEqual(1, foundManufacturers.Count);
            Assert.AreEqual(foundManufacturers.First().ManufacturerName, newManufacturer.ManufacturerName);

            ManufacturerService.DeleteByGuid(foundManufacturers.First().ManufacturerId);
        }

        [TestMethod]
        public void CheckDuplicateManufacturerTest()
        {
            var uniqueName = ManufacturerService.CheckForExistingManufacturerName(TestManufacturerName);
            Assert.AreEqual(TestManufacturer.ManufacturerName, uniqueName.ManufacturerName);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateNameException))]
        public void InsertManufacturerThatAlreadyExistsTest()
        {
            TestManufacturer.IsDeleted = true;
            ManufacturerService.Upsert(TestManufacturer, TestUserPrincipal);
        }

        [TestMethod]
        public void InsertManufacturerWithNameThatIsSoftDeletedTest()
        {
            TestManufacturer.IsDeleted = true;
            ManufacturerService.Update(TestManufacturer);

            var newManufacturer = new Manufacturer { ManufacturerName = TestManufacturerName};
            ManufacturerService.Upsert(newManufacturer, TestUserPrincipal);

            var foundManufacturers = ManufacturerService.Search(TestManufacturerName).ToList();

            Assert.AreEqual(1, foundManufacturers.Count);
            Assert.AreEqual(foundManufacturers.First().ManufacturerName, TestManufacturer.ManufacturerName);
            Assert.IsFalse(foundManufacturers.First().IsDeleted);
        }

        [TestMethod]        
        public void SoftDeleteManufacturerTest()
        {
            var foundManufacturers = ManufacturerService.Search(TestManufacturerName).ToList();

            Assert.AreEqual(1, foundManufacturers.Count);
            ManufacturerService.SoftDelete(foundManufacturers.First().ManufacturerId, TestUserPrincipal);
            
            var softDeletedManufacturer = ManufacturerService.Search(TestManufacturerName).ToList();
            Assert.AreEqual(1,softDeletedManufacturer.Count);
            Assert.IsTrue(softDeletedManufacturer.First().IsDeleted);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SoftDeleteManufacturerThatDoesNotExistTest()
        {
            var guid = Guid.NewGuid();
            ManufacturerService.SoftDelete(guid, TestUserPrincipal);
        }

        [TestMethod]
        public void UniqueNameCheckNonDuplicateManufacturerTest()
        {
            var uniqueName = ManufacturerService.CheckForExistingManufacturerName("Testing Manufacturer");

            Assert.IsNull(uniqueName);
        }

        [TestMethod]
        public void UpdateManufacturerUsingUpsertTest()
        {
            TestManufacturer.ManufacturerName = TestManufacturerName + " updated";
            ManufacturerService.Upsert(TestManufacturer, TestUserPrincipal);
            var foundManufacturers = ManufacturerService.Search(TestManufacturerName + " updated").ToList();

            Assert.AreEqual(1, foundManufacturers.Count);
            Assert.AreEqual(foundManufacturers.First().ManufacturerName, TestManufacturer.ManufacturerName);
        }
    }
}
