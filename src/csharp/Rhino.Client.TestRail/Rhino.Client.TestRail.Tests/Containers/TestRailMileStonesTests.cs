/*
 * CHANGE LOG - keep only last 5 threads
 *    - AddMilestone is a precondition and no need to test it directly
 *    - DeleteMilestone is a postcondition and no need to test it directly
 * 
 * on-line resources
 */
using Rhino.Client.TestRail.Clients;
using Rhino.Client.TestRail.Contracts;
using Rhino.Client.TestRail.Tests.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Rhino.Client.TestRail.Tests.Containers
{
    [TestClass]
    public class TestRailMileStonesTests : TestsContainer
    {
        // members: state
        private static TestRailMilestonesClient client;
        private static TestRailMileStone mileStone;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            client = Repository.ClientFactory.CreateClient<TestRailMilestonesClient>();
            mileStone = Repository.CreateMilestone();
            context.WriteLine($"class [{nameof(TestRailMileStonesTests)}] initialize completed");
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            client.DeleteMilestone(mileStone.Id);
            Console.WriteLine($"class [{nameof(TestRailMileStonesTests)}] cleanup completed");
        }

        [TestMethod]
        public void GetMilestone()
            => Assert.IsTrue(client.GetMileStone(mileStone.Id).Name.Equals(Constants.TEST_MILESTONE, Constants.STRING_COMPARE));

        [TestMethod]
        public void GetMilestonesByProjectId()
            => Assert.IsTrue(client.GetMileStones(Repository.TestRailProject.Id).Any());

        [TestMethod]
        public void GetMilestonesByProjectName()
            => Assert.IsTrue(client.GetMileStones(Repository.TestRailProject.Name).Any());

        [TestMethod]
        public void UpdateMilestone()
        {
            var expected = $"just updated {DateTime.Now.ToString("yyyyMMddhhmmssfff")}";
            mileStone.Description = expected;
            var actual = client.UpdateMilestone(mileStone.Id, mileStone);
            Assert.IsTrue(actual.Description.Equals(expected));
        }
    }
}