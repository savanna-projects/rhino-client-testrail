/*
 * CHANGE LOG - keep only last 5 threads
 *    - AddCase is a precondition and no need to test it directly
 *    - DeleteCase is a post condition and no need to test it directly
 * 
 * on-line resources
 */
using Automation.TestRail.Clients;
using Automation.TestRail.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Automation.TestRail.Tests.Containers
{
    [TestClass]
    public class TestRailCasesClientTests : TestsContainer
    {
        // members: state
        private static TestRailCasesClient client;
        private static TestRailSuitesClient suitesClient;
        private static TestRailSectionsClient sectionsClient;
        private static TestRailSuite suite;
        private static TestRailSection section;
        private static TestRailCase testCase;
        private static IEnumerable<TestRailCase> testCases;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            client = Repository.ClientFactory.CreateClient<TestRailCasesClient>();
            suitesClient = Repository.ClientFactory.CreateClient<TestRailSuitesClient>();
            sectionsClient = Repository.ClientFactory.CreateClient<TestRailSectionsClient>();

            suite = Repository.CreateTestSuite();
            section = Repository.CreateSection(suite.Id);
            testCases = Repository.CreateTestCases(5, section.Id);
            testCase = testCases.FirstOrDefault();
            context.WriteLine($"class [{nameof(TestRailSuitesClientTests)}] initialize completed");
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            testCases.AsParallel().ForAll(i => client.DeleteCase(i.Id));
            sectionsClient.DeleteSection(section.Id);
            suitesClient.DeleteSuite(suite.Id);
            Console.WriteLine($"class [{nameof(TestRailSuitesClientTests)}] cleanup completed");
        }

        [TestMethod]
        public void GetCase()
        {
             Assert.IsTrue(client.GetCase(testCase.Id).Id == testCase.Id);
        }

        [TestMethod]
        public void GetCasesBySuite()
        {
            var dto = new GetCasesArguments
            {
                ProjectId = Repository.TestRailProject.Id,
                SuiteId = suite.Id
            };
            Assert.IsTrue(client.GetCases(dto).Any());
        }

        [TestMethod]
        public void GetCasesBySection()
        {
            var dto = new GetCasesArguments
            {
                ProjectId = Repository.TestRailProject.Id,
                SuiteId = suite.Id,
                SectionId = section.Id
            };
            Assert.IsTrue(client.GetCases(dto).Any());
        }

        [TestMethod]
        public void GetCasesByOffsetAndLimit()
        {
            var dto = new GetCasesArguments
            {
                ProjectId = Repository.TestRailProject.Id,
                SuiteId = suite.Id,
                Offset = 3,
                Limit = 2
            };
            Assert.IsTrue(client.GetCases(dto).All(i => i.DisplayOrder == 4 || i.DisplayOrder == 5));
        }

        [TestMethod]
        public void GetCasesByFilter()
        {
            var dto = new GetCasesArguments
            {
                ProjectId = Repository.TestRailProject.Id,
                SuiteId = suite.Id,
                Filter = "3"
            };
            Assert.IsTrue(client.GetCases(dto).All(i => i.Title.Contains("3")));
        }

        [TestMethod]
        public void UpdateCase()
        {
            var expected = $"just updated {DateTime.Now.ToString("yyyyMMddhhmmssfff")}";
            testCase.Title = expected;
            var actual = client.UpdateCase(testCase.Id, testCase);
            Assert.IsTrue(actual.Title.Equals(expected));
        }
    }
}