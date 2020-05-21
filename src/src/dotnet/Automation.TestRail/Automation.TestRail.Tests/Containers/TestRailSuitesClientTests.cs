/*
 * CHANGE LOG - keep only last 5 threads
 *    - AddSuite is a precondition and no need to test it directly
 *    - DeleteSuite is a postcondition and no need to test it directly
 * 
 * on-line resources
 */
using Automation.TestRail.Clients;
using Automation.TestRail.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Automation.TestRail.Tests.Containers
{
    [TestClass]
    public class TestRailSuitesClientTests : TestsContainer
    {
        // members: state
        private static TestRailSuitesClient client;
        private static TestRailSuite testSuite;

        // constructor
        public TestRailSuitesClientTests()
        {
            client = Repository.ClientFactory.CreateClient<TestRailSuitesClient>();
            testSuite = Repository.CreateTestSuite();
        }

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            client = Repository.ClientFactory.CreateClient<TestRailSuitesClient>();
            testSuite = Repository.CreateTestSuite();
            context.WriteLine($"class [{nameof(TestRailSuitesClientTests)}] initialize completed");
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            client.DeleteSuite(testSuite.Id);
            Console.WriteLine($"class [{nameof(TestRailSuitesClientTests)}] cleanup completed");
        }

        [TestMethod]
        public void GetSuitesByProjectId()
            => Assert.IsTrue(client.GetSuites(Repository.TestRailProject.Id).Any());

        [TestMethod]
        public void GetSuitesByProjectName()
            => Assert.IsTrue(client.GetSuites(Repository.TestRailProject.Name).Any());

        [TestMethod]
        public void GetSuite() => Assert.IsTrue(client.GetSuite(testSuite.Id).Id > 0);

        [TestMethod]
        public void UpdateSuite()
        {
            var expected = $"just updated {DateTime.Now.ToString("yyyyMMddhhmmssfff")}";
            testSuite.Description = expected;
            var actual = client.UpdateSuite(testSuite.Id, testSuite);
            Assert.IsTrue(actual.Description.Equals(expected));
        }
    }
}