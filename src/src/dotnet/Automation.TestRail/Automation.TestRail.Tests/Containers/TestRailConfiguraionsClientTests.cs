/*
 * CHANGE LOG - keep only last 5 threads
 *    - AddConfiguration is a precondition and no need to test it directly
 *    - DeleteConfiguration is postcondition and no need to test it directly
 * 
 * on-line resources
 */
using Automation.TestRail.Clients;
using Automation.TestRail.Contracts;
using Automation.TestRail.Tests.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Automation.TestRail.Tests.Containers
{
    [TestClass]
    public class TestRailConfiguraionsClientTests : TestsContainer
    {
        // members: state
        private static TestRailConfigurationsClient client;
        private static TestRailConfiguration testRailConfiguration;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            client = Repository.ClientFactory.CreateClient<TestRailConfigurationsClient>();
            testRailConfiguration = Repository.CreateConfiguration();
            context.WriteLine($"class [{nameof(TestRailConfiguraionsClientTests)}] initialize completed");
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            client.DeleteConfig(testRailConfiguration.Id);
            client.DeleteConfigGroup(testRailConfiguration.GroupId);
            Console.WriteLine($"class [{nameof(TestRailConfiguraionsClientTests)}] cleanup completed");
        }

        [TestMethod]
        public void GetConfigsByProjectId()
            => Assert.IsTrue(client.GetConfigs(Repository.TestRailProject.Id).Any());

        [TestMethod]
        public void GetConfigsByProjectName()
            => Assert.IsTrue(client.GetConfigs(Repository.TestRailProject.Name).Any());

        [TestMethod]
        public void UpdateConfig()
        {
            var expected = $"just updated {DateTime.Now.ToString("yyyyMMddhhmmssfff")}";
            testRailConfiguration.Name = expected;
            client.UpdateConfig(testRailConfiguration.Id, testRailConfiguration);
            var actual = client
                .GetConfigs(Repository.TestRailProject.Id)
                .SelectMany(i => i.Configs)
                .Where(i => i.Name.Equals(expected, Constants.STRING_COMPARE));
            Assert.IsTrue(actual.Any());
        }
    }
}