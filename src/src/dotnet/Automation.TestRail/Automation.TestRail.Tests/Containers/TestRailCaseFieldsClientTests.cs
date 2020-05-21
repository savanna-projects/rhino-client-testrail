/*
 * CHANGE LOG - keep only last 5 threads
 *    - AddCaseField is a precondition and no need to test it directly
 *    - DeleteCaseField is a post condition and no need to test it directly
 * 
 * on-line resources
 */
using Automation.TestRail.Clients;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Automation.TestRail.Tests.Containers
{
    [TestClass]
    public class TestRailCaseFieldsClientTests : TestsContainer
    {
        // members: state
        private static TestRailCaseFieldsClient client;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            client = Repository.ClientFactory.CreateClient<TestRailCaseFieldsClient>();
            context.WriteLine($"class [{nameof(TestRailSuitesClientTests)}] initialize completed");
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            Console.WriteLine($"class [{nameof(TestRailSuitesClientTests)}] cleanup completed");
        }

        [TestMethod]
        public void GetCaseFields()
            => Assert.IsTrue(client.GetCaseFields().Length > 0);
    }
}