/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 */
using Rhino.Client.TestRail.Clients;
using Rhino.Client.TestRail.Contracts;
using System.Collections.Generic;

namespace Rhino.Client.TestRail.Tests.Domain.Internal
{
    internal interface ITestingRepository
    {
        TestRailClientFactory ClientFactory { get; }
        TestRailProject TestRailProject { get; }

        TestRailConfiguration CreateConfiguration();
        TestRailSection CreateSection(int suiteId);
        TestRailSuite CreateTestSuite();
        TestRailMileStone CreateMilestone();
        TestRailPlan CreatePlan(int suiteId, int configurationId);
        TestRailCase CreateCase(int sectionId, int suiteId);
        TestRailPlanEntry CreatePlanEntry(int planId, int configurationId, int suiteId);
        TestRailResult CreateResultForCase(int runId, int caseId);
        IEnumerable<TestRailTest> GetTests(int runId);
        IEnumerable<TestRailCase> CreateTestCases(int numberOfTestCases, int sectionId);
        TestRailCaseField CreateCaseField();
    }
}