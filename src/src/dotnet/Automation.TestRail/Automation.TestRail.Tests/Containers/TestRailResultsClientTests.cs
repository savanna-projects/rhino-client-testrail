/*
 * CHANGE LOG - keep only last 5 threads
 *    - AddResultForCase is a precondition and no need to test it directly
 *    - AddAttachmentToResultForCase is a precondition and no need to test it directly
 *    - DeleteAttachment is a post condition and no need to test it directly
 * 
 * on-line resources
 */
using Automation.TestRail.Clients;
using Automation.TestRail.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace Automation.TestRail.Tests.Containers
{
    [TestClass]
    public class TestRailResultsClientTests : TestsContainer
    {
        // constants
        private static readonly string ATTACHMENT = $"{Environment.CurrentDirectory}\\tr-att.txt";

        // members: state
        private static TestRailAttachmentsClient attachmentsClient;
        private static TestRailResultsClient client;
        private static TestRailPlan plan;
        private static TestRailSection section;
        private static TestRailSuite suite;
        private static TestRailCase @case;
        private static TestRailConfiguration configuration;
        private static TestRailPlanEntry entry;
        private static TestRailRun run;
        private static TestRailTest test;
        private static TestRailResult result;
        private static TestRailAttachment attachment;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            client = Repository.ClientFactory.CreateClient<TestRailResultsClient>();
            attachmentsClient = Repository.ClientFactory.CreateClient<TestRailAttachmentsClient>();
            suite = Repository.CreateTestSuite();
            section = Repository.CreateSection(suite.Id);
            @case = Repository.CreateCase(section.Id, suite.Id);
            configuration = Repository.CreateConfiguration();
            plan = Repository.CreatePlan(suite.Id, configuration.Id);
            entry = Repository.CreatePlanEntry(plan.Id, configuration.Id, suite.Id);
            run = entry.Runs.FirstOrDefault();
            result = Repository.CreateResultForCase(run.Id, @case.Id);
            test = Repository.GetTests(run.Id).FirstOrDefault();
            File.WriteAllText(ATTACHMENT, "uploaded by an automation test, for testing purposes only");
            attachment = attachmentsClient.AddAttachmentToResultForCase(result.Id, @case.Id, ATTACHMENT);
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
            attachmentsClient.DeleteAttachment(attachment.Id);
            File.Delete(ATTACHMENT);
            Console.WriteLine($"class [{nameof(TestRailSuitesClientTests)}] cleanup completed");
        }

        [TestMethod]
        public void GetResults() => Assert.IsTrue(client.GetResults(test.Id).Any());

        [TestMethod]
        public void GetResultsForCase()
            => Assert.IsTrue(client.GetResultsForCase(run.Id, @case.Id).Any());

        [TestMethod]
        public void GetResultsForRun()
            => Assert.IsTrue(client.GetResultsForRun(run.Id).Any());

        [TestMethod]
        public void AddResult()
        {
            var actual = client.AddResult(test.Id, new TestRailResult { StatusId = 1 });
            Assert.IsTrue(actual.Id != 0);
        }

        [TestMethod]
        public void AddResults()
        {
            var content = new[]
            {
                new TestRailResult { StatusId = 1, TestId = test.Id },
                new TestRailResult { StatusId = 5, TestId = test.Id },
            };
            var actual = client.AddResults(run.Id, content);
            Assert.IsTrue(actual.Count() == 2);
        }

        [TestMethod]
        public void AddResultsForCases()
        {
            var content = new[]
            {
                new TestRailResult { StatusId = 1, CaseId =@case.Id },
                new TestRailResult { StatusId = 5, CaseId =@case.Id },
            };
            var actual = client.AddResultsForCases(run.Id, content);
            Assert.IsTrue(actual.Count() == 2);
        }

        [TestMethod]
        public void AddAttachmentToResult()
            => Assert.IsTrue(attachmentsClient.AddAttachmentToResult(result.Id, ATTACHMENT).AttachmentId != 0);

        [TestMethod]
        public void GetAttachmentsForCase()
            => Assert.IsTrue(attachmentsClient.GetAttachmentsForCase(@case.Id).Any());

        [TestMethod]
        public void GetAttachmentsForTest()
            => Assert.IsTrue(attachmentsClient.GetAttachmentsForTest(test.Id).Any());

        [TestMethod]
        public void GetAttachement()
            => Assert.IsTrue(attachmentsClient.GetAttachment(attachment.AttachmentId).Length > 0);
    }
}