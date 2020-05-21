/*
 * CHANGE LOG - keep only last 5 threads
 *    - AddProject is a precondition and no need to test it directly
 *    - DeleteProject is postcondition and no need to test it directly
 * 
 * on-line resources
 */
using Automation.TestRail.Clients;
using Automation.TestRail.Tests.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Automation.TestRail.Tests.Containers
{
    [TestClass]
    public class TestRailProjectsClientTests : TestsContainer
    {
        // members: state
        private readonly TestRailProjectsClient client;

        // constructor
        public TestRailProjectsClientTests()
        {
            client = Repository.ClientFactory.CreateClient<TestRailProjectsClient>();
        }

        [TestMethod]
        public void GetProjects() => Assert.IsTrue(client.GetProjects().Any());

        [TestMethod]
        public void GetProjectById()
            => Assert.IsTrue(client.GetProject(Repository.TestRailProject.Id).Id > 0);

        [TestMethod]
        public void GetProjectByName()
            => Assert.IsTrue(Repository.TestRailProject.Name.Equals(Constants.TEST_PROJECT, Constants.STRING_COMPARE));

        [TestMethod]
        public void UpdateProject()
        {
            var expected = $"just updated {DateTime.Now.ToString("yyyyMMddhhmmssfff")}";
            Repository.TestRailProject.Announcement = expected;
            var actual = client.UpdateProject(Repository.TestRailProject.Id, Repository.TestRailProject);
            Assert.IsTrue(actual.Announcement.Equals(expected));
        }
    }
}