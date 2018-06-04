using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartHomeAutomation.Domain.Models.Device;

namespace SmartHomeAutomation.Services.Tests
{
    [TestClass]
    public class ManufacturerServiceTests : TestBase
    {
        [TestInitialize]
        public void TestInitialize()
        {
            TestManufacturer = CreateTestManufacturer();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            DeleteTestManufacturer(TestManufacturer);
        }

        [TestMethod]        
        public void CreateNewManufacturerWithUpsertTest()
        {
            var newManufacturer = new Manufacturer {ManufacturerName = "New Upsert Test Manufacturer"};
            ManufacturerService.Upsert(newManufacturer, TestUser);
            var foundManufacturers = ManufacturerService.Search("New Upsert Test Manufacturer").ToList();

            Assert.AreEqual(1, foundManufacturers.Count);
            Assert.AreEqual(foundManufacturers.First().ManufacturerName, newManufacturer.ManufacturerName);

            ManufacturerService.DeleteByGuid(foundManufacturers.First().ManufacturerId);
        }

        [TestMethod]        
        public void SoftDeleteManufacturerTest()
        {
            var foundManufacturers = ManufacturerService.Search("Test Manufacturer").ToList();

            Assert.AreEqual(1, foundManufacturers.Count);
            ManufacturerService.SoftDelete(foundManufacturers.First().ManufacturerId, TestUser);
            
            var softDeletedManufacturer = ManufacturerService.Search("Test Manufacturer").ToList();
            Assert.AreEqual(1,softDeletedManufacturer.Count);
            Assert.IsTrue(softDeletedManufacturer.First().IsDeleted);
        }

        [TestMethod]
        public void UniqueNameCheckDuplicateManufacturer()
        {
            var uniqueName = ManufacturerService.CheckForExistingManufacturerName("Test Manufacturer");

            Assert.AreEqual(TestManufacturer.ManufacturerName, uniqueName.ManufacturerName);
        }

        [TestMethod]
        public void UniqueNameCheckNonDuplicateManufacturer()
        {
            var uniqueName = ManufacturerService.CheckForExistingManufacturerName("Testing Manufacturer");

            Assert.IsNull(uniqueName);
        }

        [TestMethod]
        public void UpdateManufacturerUsingUpsert()
        {
            TestManufacturer.ManufacturerName = "Test Manufacturer updated";
            ManufacturerService.Upsert(TestManufacturer, TestUser);
            var foundManufacturers = ManufacturerService.Search("Test Manufacturer updated").ToList();

            Assert.AreEqual(1, foundManufacturers.Count);
            Assert.AreEqual(foundManufacturers.First().ManufacturerName, TestManufacturer.ManufacturerName);
        }
    }
}
