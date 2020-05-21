/*
 * CHANGE LOG - keep only last 5 threads
 *    - AddSection is a precondition and no need to test it directly
 *    - DeleteSection is a postcondition and no need to test it directly
 * 
 * on-line resources
 */
using Rhino.Client.TestRail.Clients;
using Rhino.Client.TestRail.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Rhino.Client.TestRail.Tests.Containers
{
    [TestClass]
    public class TestRailSectionsClientTests : TestsContainer
    {
        // members: state
        private static TestRailSectionsClient client;
        private static TestRailSuite testSuite;
        private static TestRailSection section;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            client = Repository.ClientFactory.CreateClient<TestRailSectionsClient>();
            testSuite = Repository.CreateTestSuite();
            section = Repository.CreateSection(testSuite.Id);
            context.WriteLine($"class [{nameof(TestRailSectionsClientTests)}] initialize completed");
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            client.DeleteSection(section.Id);
            Repository.ClientFactory.CreateClient<TestRailSuitesClient>().DeleteSuite(testSuite.Id);
            Console.WriteLine($"class [{nameof(TestRailSectionsClientTests)}] cleanup completed");
        }

        [TestMethod]
        public void GetSection() => Assert.IsTrue(client.GetSection(section.Id).Id != 0);

        [TestMethod]
        public void GetSections()
            => Assert.IsTrue(client.GetSections(Repository.TestRailProject.Id, testSuite.Id).Length > 0);

        [TestMethod]
        public void UpdateSection()
        {
            var expected = $"just updated {DateTime.Now.ToString("yyyyMMddhhmmssfff")}";
            section.Description = expected;
            var actual = client.UpdateSection(section.Id, section);
            Assert.IsTrue(actual.Description.Equals(expected));
        }
    }
}