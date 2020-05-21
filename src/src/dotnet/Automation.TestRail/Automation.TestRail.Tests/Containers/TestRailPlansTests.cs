/*
 * CHANGE LOG - keep only last 5 threads
 *    - AddPlan is a precondition and no need to test it directly
 *    - AddPlanEntry is a precondition and no need to test it directly
 *    - DeletePlan is a postcondition and no need to test it directly
 *    - DeletePlanEntry is a postcondition and no need to test it directly
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
    public class TestRailPlansTests : TestsContainer
    {
        // members: state
        private static TestRailPlansClient client;
        private static TestRailPlan plan;
        private static TestRailSection section;
        private static TestRailSuite suite;
        private static TestRailCase @case;
        private static TestRailConfiguration configuration;
        private static TestRailPlanEntry entry;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            client = Repository.ClientFactory.CreateClient<TestRailPlansClient>();
            suite = Repository.CreateTestSuite();
            section = Repository.CreateSection(suite.Id);
            @case = Repository.CreateCase(section.Id, suite.Id);
            configuration = Repository.CreateConfiguration();
            plan = Repository.CreatePlan(suite.Id, configuration.Id);
            entry = Repository.CreatePlanEntry(plan.Id, configuration.Id, suite.Id);
            context.WriteLine($"class [{nameof(TestRailSuitesClientTests)}] initialize completed");
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            // initialize clients
            var plansClient = Repository.ClientFactory.CreateClient<TestRailPlansClient>();
            var sectionsClient = Repository.ClientFactory.CreateClient<TestRailSectionsClient>();
            var suitesClient = Repository.ClientFactory.CreateClient<TestRailSuitesClient>();
            var casesClient = Repository.ClientFactory.CreateClient<TestRailCasesClient>();
            var configurationsClient = Repository.ClientFactory.CreateClient<TestRailConfigurationsClient>();

            // clean entities
            plansClient.DeletePlanEntry(plan.Id, entry.Id);
            plansClient.DeletePlan(plan.Id);
            casesClient.DeleteCase(@case.Id);
            sectionsClient.DeleteSection(section.Id);
            suitesClient.DeleteSuite(suite.Id);
            configurationsClient.DeleteConfig(configuration.Id);
            configurationsClient.DeleteConfigGroup(configuration.GroupId);
            Console.WriteLine($"class [{nameof(TestRailSuitesClientTests)}] cleanup completed");
        }

        [TestMethod]
        public void GetPlan() => Assert.IsTrue(client.GetPlan(plan.Id).Id > 0);

        [TestMethod]
        public void GetPlansByProjectId()
            => Assert.IsTrue(client.GetPlans(Repository.TestRailProject.Id).Any());

        [TestMethod]
        public void GetPlansByProjectName()
            => Assert.IsTrue(client.GetPlans(Repository.TestRailProject.Name).Any());

        // updating is not possible after plan is closed
        // this integration tests solves the order dependent tests
        [TestMethod]
        public void IntegrationTest()
        {
            Assert.IsTrue(UpdatePlan(), $"[{nameof(UpdatePlan)}] failed");
            Assert.IsTrue(UpdatePlanEntry(), $"[{nameof(UpdatePlanEntry)}] failed");
            Assert.IsTrue(ClosePlan(), $"[{nameof(ClosePlan)}] failed");
        }

        private bool UpdatePlan()
        {
            var expected = $"just updated {DateTime.Now.ToString("yyyyMMddhhmmssfff")}";
            plan.Description = expected;
            var actual = client.UpdatePlan(plan.Id, plan);
            return actual.Description.Equals(expected);
        }

        private bool UpdatePlanEntry()
        {
            var expected = $"just updated {DateTime.Now.ToString("yyyyMMddhhmmssfff")}";
            entry.Name = expected;
            entry.IncludeAll = true;
            entry.ConfigIds = new[] { configuration.Id };
            entry.CaseIds = new int[0];
            var actual = client.UpdatePlanEntry(plan.Id, entry.Id, entry);
            return actual.Name.Equals(expected);
        }

        private bool ClosePlan() => client.ClosePlan(plan.Id).IsCompleted;
    }
}