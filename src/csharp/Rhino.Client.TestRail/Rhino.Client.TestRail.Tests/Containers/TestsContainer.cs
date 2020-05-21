/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 */
using Rhino.Client.TestRail.Clients;
using Rhino.Client.TestRail.Tests.Domain;
using Rhino.Client.TestRail.Tests.Domain.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Rhino.Client.TestRail.Tests.Containers
{
    [TestClass]
    public abstract class TestsContainer
    {
        // reset ALM testing project
        [AssemblyInitialize]
        public static void AssemblySetup(TestContext context)
        {
            Repository = new TestingRepository();
            context.WriteLine($"assembly [{nameof(TestsContainer)}] initialize completed");
        }

        // reset ALM testing project
        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            var client = Repository.ClientFactory.CreateClient<TestRailProjectsClient>();
            client.DeleteProject(Repository.TestRailProject.Id);
            Console.WriteLine($"assembly [{nameof(TestsContainer)}] cleanup completed");
        }

        protected TestsContainer() { }

        /// <summary>
        /// Gets this implementation for testing repository
        /// </summary>
        internal static ITestingRepository Repository { get; private set; }
    }
}