using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepositoryDependency.Repository;
using System.Linq;
using RepositoryDependency.Models;

namespace RepositoryTest
{
    [TestClass]
    public class EmployeeInfoControllerTest
    {
        EmployeeInfoRepository Repo;

        [TestInitialize]
        public void TestSetup()
        {
            EmployeeDbContext db = new EmployeeDbContext();
            // System.Data.Entity.Database.SetInitializer(db);
            Repo = new EmployeeInfoRepository(db);
        }
        
        [TestMethod]
        public void IsRepositoryInitalizeWithValidNumberOfData()
        {
            var result = Repo.Get();
            Assert.IsNotNull(result);
            var numberOfRecords = result.ToList().Count;
            Assert.AreEqual(2, numberOfRecords);
        }

        [TestMethod]
        public void IsRepositoryWithValidNumberOfDataRecords()
        {
            var result = Repo.Get();
            Assert.IsNotNull(result);
            var numberOfRecords = result.ToList().Count;
            Assert.AreEqual(3, numberOfRecords);
        }
    }
}
