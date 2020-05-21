/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 */
using Automation.TestRail.Clients;
using Automation.TestRail.Contracts;
using Automation.TestRail.Tests.Domain.Internal;
using Automation.TestRail.Tests.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Automation.TestRail.Tests.Domain
{
    internal class TestingRepository : ITestingRepository
    {
        public TestingRepository()
        {
            ClientFactory = new TestRailClientFactory(Constants.SERVER, Constants.USER, Constants.PASSWORD);
            TestRailProject = GetProject();
        }

        /// <summary>
        /// Gets the ALM testing project (preconditions for all tests)
        /// </summary>
        public TestRailProject TestRailProject { get; }

        /// <summary>
        /// Gets this client factory (use for generating the different test-rail clients)
        /// </summary>
        public TestRailClientFactory ClientFactory { get; }

        /// <summary>
        /// Creates a new configuration
        /// </summary>
        /// <returns>Test-Rail configuration entity</returns>
        public TestRailConfiguration CreateConfiguration() => GetConfiguration();

        /// <summary>
        /// Creates a new test-section
        /// </summary>
        /// <param name="suiteId">The id of the test suite</param>
        /// <returns>Test-Rail section object</returns>
        public TestRailSection CreateSection(int suiteId) => GetSection(suiteId);

        /// <summary>
        /// Creates a new test suite
        /// </summary>
        /// <returns>Test-Rail test suite object</returns>
        public TestRailSuite CreateTestSuite() => GetSuite();

        /// <summary>
        /// Creates a new milestone under this project
        /// </summary>
        /// <returns>Test-Rail milestone object</returns>
        public TestRailMileStone CreateMilestone()
        {
            // initialize client
            var client = ClientFactory.CreateClient<TestRailMilestonesClient>();

            // create entity
            return client.AddMilestone(TestRailProject.Id, new TestRailMileStone { Name = Constants.TEST_MILESTONE });
        }

        /// <summary>
        /// Creates a new test-plan under this project
        /// </summary>
        /// <param name="suiteId">suite id to add into this test-plan</param>
        /// <param name="configurationId">configuration it to add into this test-plan</param>
        /// <returns>Test-Rail Plan object</returns>
        public TestRailPlan CreatePlan(int suiteId, int configurationId)
        {
            // initialize client
            var client = ClientFactory.CreateClient<TestRailPlansClient>();

            // create entity
            return client.AddPlan(TestRailProject.Id, new TestRailPlan
            {
                Name = Constants.TEST_PLAN
            });
        }

        /// <summary>
        /// Creates a new test-plan-entry under this test-plan
        /// </summary>
        /// <param name="planId">plan id to add entry to</param>
        /// <param name="configurationId">configuration it to add into this test-plan-entry</param>
        /// <param name="suiteId">suite id to add into this test-plan-entry</param>
        /// <returns>Test-Rail Plan-Entry object</returns>
        public TestRailPlanEntry CreatePlanEntry(int planId, int configurationId, int suiteId)
        {
            // initialize client
            var client = ClientFactory.CreateClient<TestRailPlansClient>();

            // create entity
            return client.AddPlanEntry(planId, new TestRailPlanEntry
            {
                Name = Constants.TEST_PLAN_ENTRY,
                ConfigIds = new[] { configurationId },
                SuiteId = suiteId,
                IncludeAll = true,
                Runs = new[]
                {
                    new TestRailRun { SuiteId = suiteId, ConfigIds = new[] { configurationId }, IncludeAll = true }
                }
            });
        }

        /// <summary>
        /// Creates a new test-case under this project
        /// </summary>
        /// <returns>Test-Rail Case object</returns>
        public TestRailCase CreateCase(int sectionId, int suiteId)
        {
            // initialize client
            var client = ClientFactory.CreateClient<TestRailCasesClient>();

            // create entity
            return client.AddCase(sectionId, new TestRailCase
            {
                SuiteId = suiteId,
                Title = Constants.TEST_CASE
            });
        }

        /// <summary>
        /// Adds a new test result, comment or assigns a test (for a test run and case combination)
        /// </summary>
        /// <param name="runId">The ID of the test run</param>
        /// <param name="caseId">The ID of the test case</param>
        /// <returns>Test-Rail result object</returns>
        public TestRailResult CreateResultForCase(int runId, int caseId)
        {
            // initialize client
            var client = ClientFactory.CreateClient<TestRailResultsClient>();

            // create entity
            return client.AddResultForCase(runId, caseId, new TestRailResult
            {
                StatusId = 1
            });
        }

        /// <summary>
        /// Returns a list of tests for a test run.
        /// </summary>
        /// <param name="runId">The ID of the test run</param>
        /// <returns>Test-Rail test collection</returns>
        public IEnumerable<TestRailTest> GetTests(int runId)
        {
            // initialize client
            var client = ClientFactory.CreateClient<TestRailTestsClient>();

            // create entity
            return client.GetTests(runId);
        }

        /// <summary>
        /// Creates a number of test cases under a section
        /// </summary>
        /// <param name="numberOfTestCases">Number of total test cases to create</param>
        /// <param name="sectionId">Section under which to create test cases</param>
        /// <returns>Created test cases</returns>
        public IEnumerable<TestRailCase> CreateTestCases(int numberOfTestCases, int sectionId)
        {
            // initialize client
            var client = ClientFactory.CreateClient<TestRailCasesClient>();

            var testCases = new List<TestRailCase>();
            for (int i = 0; i < numberOfTestCases; i++)
            {
                var title = nameof(TestRailCasesClient) + $"-{i}";
                var @case = client.AddCase(sectionId, new TestRailCase { Title = title });
                testCases.Add(@case);
            }
            return testCases;
        }

        /// <summary>
        /// Creates a new test case custom field.
        /// </summary>
        /// <returns>Created case field</returns>
        public TestRailCaseField CreateCaseField()
        {
            // create data
            var caseField = new TestRailCaseField
            {
                Type = "Multiselect",
                Name = "my_multiselect",
                Label = "My Multiselect",
                Description = "my custom Multiselect description",
                IncludeAll = true
            }
            .AddConfig(new TestRailCaseFieldConfig
            {
                Context = new Dictionary<string, object>
                {
                    ["is_global"] = false,
                    ["project_ids"] = new[] { TestRailProject.Id }
                },
                Options = new Dictionary<string, object>
                {
                    ["is_required"] = false,
                    ["items"] = $"1, one{Environment.NewLine}2, Two"
                }
            })
            .Build();

            // generate client & post request
            var client = ClientFactory.CreateClient<TestRailCaseFieldsClient>();
            return client.AddCaseField(caseField);
        }

        // initialize ALM testing project
        private TestRailProject GetProject()
        {
            // initialize client
            var client = ClientFactory.CreateClient<TestRailProjectsClient>();

            // shortcuts
            const string P = Constants.TEST_PROJECT;
            const StringComparison C = Constants.STRING_COMPARE;

            // verify entity
            var project = client.GetProjects().FirstOrDefault(i => i.Name.Equals(P, C));
            if (project != default(TestRailProject))
            {
                client.DeleteProject(project.Id);
            }

            // create entity
            return client.AddProject(new TestRailProject { Name = Constants.TEST_PROJECT, SuiteMode = 3 });
        }

        // initialize ALM test-suite
        private TestRailSuite GetSuite()
        {
            // initialize client
            var client = ClientFactory.CreateClient<TestRailSuitesClient>();

            // create entity
            return client.AddSuite(TestRailProject.Id, new TestRailSuite { Name = Constants.TEST_SUITE });
        }

        // initialize ALM test-configuration
        private TestRailConfiguration GetConfiguration()
        {
            // initialize client
            var client = ClientFactory.CreateClient<TestRailConfigurationsClient>();

            // validating: configuration group
            client.AddConfigGroup(TestRailProject.Id, new TestRailConfigurationGroup { Name = Constants.TEST_CONFIGURATION_GROUP });
            var configGroup = client.GetConfigs(TestRailProject.Id).FirstOrDefault();

            // create entity
            return client.AddConfig(configGroup.Id, new TestRailConfiguration
            {
                Name = Constants.TEST_CONFIGURATION,
                ProjectId = TestRailProject.Id
            });
        }

        // initialize ALM section
        private TestRailSection GetSection(int suiteId)
        {
            // initialize client
            var client = ClientFactory.CreateClient<TestRailSectionsClient>();

            // create entity
            return client.AddSection(TestRailProject.Id, new TestRailSection
            {
                Name = Constants.TEST_SECTION,
                SuiteId = suiteId
            });
        }
    }
}