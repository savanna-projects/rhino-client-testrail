/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 * http://docs.gurock.com/testrail-api2/reference-results
 */
using Rhino.Client.TestRail.Contracts;
using Rhino.Client.TestRail.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Rhino.Client.TestRail.Clients
{
    /// <summary>
    /// Use the following API methods to request details about test results and to add new test results.
    /// </summary>
    public class TestRailResultsClient : TestRailClient
    {
        #region *** constructors  ***
        /// <summary>
        /// Creates a new instance of this client
        /// </summary>
        /// <param name="testRailServer">Test-Rail server to create a client against</param>
        /// <param name="user">Test-Rail user name (account email)</param>
        /// <param name="password">Test-Rail password</param>
        public TestRailResultsClient(string testRailServer, string user, string password)
            : base(testRailServer, user, password) { }

        /// <summary>
        /// Creates a new instance of this client
        /// </summary>
        /// <param name="testRailServer">Test-Rail server to create a client against</param>
        /// <param name="user">Test-Rail user name (account email)</param>
        /// <param name="password">Test-Rail password</param>
        /// <param name="logger">Logger implementation for this client</param>
        public TestRailResultsClient(Uri testRailServer, string user, string password, TraceListener logger)
            : base(testRailServer, user, password, logger) { }
        #endregion

        #region *** pipeline: get ***
        /// <summary>
        /// Returns a list of test results for a test (up to 250).
        /// </summary>
        /// <param name="testId">The ID of the test</param>
        /// <returns>Success, the test results are returned as part of the response</returns>
        public IEnumerable<TestRailResult> GetResults(int testId)
        {
            // create command
            var command = string.Format(ApiCommands.GET_RESULTS, testId);

            // execute command
            return ExecuteGet<TestRailResult[]>(command);
        }

        /// <summary>
        /// Returns a list of test results for a test (up to 250).
        /// </summary>
        /// <param name="testId">The ID of the test</param>
        /// <param name="offset">Use :offset to skip records</param>
        /// <returns>Success, the test results are returned as part of the response</returns>
        public IEnumerable<TestRailResult> GetResults(int testId, int offset)
        {
            // create command
            var command = string.Format(ApiCommands.GET_RESULTS_OFFSET, testId, offset);

            // execute command
            return ExecuteGet<TestRailResult[]>(command);
        }

        /// <summary>
        /// Returns a list of test results for a test (up to 250).
        /// </summary>
        /// <param name="testId">The ID of the test</param>
        /// <param name="statusId">A comma-separated list of status IDs to filter by.</param>
        /// <returns>Success, the test results are returned as part of the response</returns>
        public IEnumerable<TestRailResult> GetResults(int testId, IEnumerable<int> statusId)
        {
            // create command
            var status = string.Join(",", statusId.ToArray());
            var command = string.Format(ApiCommands.GET_RESULTS_STATUS, testId, status);

            // execute command
            return ExecuteGet<TestRailResult[]>(command);
        }

        /// <summary>
        /// Returns a list of test results for a test (up to 250).
        /// </summary>
        /// <param name="testId">The ID of the test</param>
        /// <param name="offset">Use :offset to skip records</param>
        /// <param name="statusId">A comma-separated list of status IDs to filter by.</param>
        /// <returns>Success, the test results are returned as part of the response</returns>
        public IEnumerable<TestRailResult> GetResults(int testId, int offset, IEnumerable<int> statusId)
        {
            // create command
            var status = string.Join(",", statusId.ToArray());
            var command = string.Format(ApiCommands.GET_RESULTS_OFFSET_STATUS, testId, offset, status);

            // execute command
            return ExecuteGet<TestRailResult[]>(command);
        }

        /// <summary>
        /// Returns a list of test results for a test run and case combination.
        /// </summary>
        /// <param name="runId">The ID of the test run</param>
        /// <param name="caseId">The ID of the test case</param>
        /// <returns>Success, the test results are returned as part of the response</returns>
        public IEnumerable<TestRailResult> GetResultsForCase(int runId, int caseId)
        {
            // create command
            var command = string.Format(ApiCommands.GET_RESUTLS_FOR_CASE, runId, caseId);

            // execute command
            return ExecuteGet<TestRailResult[]>(command);
        }

        /// <summary>
        /// Returns a list of test results for a test run. Requires TestRail 4.0 or later.
        /// </summary>
        /// <param name="runId">The ID of the test run</param>
        /// <returns>Success, the test results are returned as part of the response</returns>
        public IEnumerable<TestRailResult> GetResultsForRun(int runId)
        {
            // create command
            var command = string.Format(ApiCommands.GET_RESUTLS_FOR_RUN, runId);

            // execute command
            return ExecuteGet<TestRailResult[]>(command);
        }
        #endregion

        #region *** pipeline: add ***
        /// <summary>
        /// Adds a new test result, comment or assigns a test.
        /// </summary>
        /// <param name="testId">The ID of the test the result should be added to</param>
        /// <param name="data">Request DTO</param>
        /// <returns>Success, the test result was created and is returned as part of the response</returns>
        /// <remarks>It's recommended to use add_results instead if you plan to add results for multiple tests.</remarks>
        public TestRailResult AddResult(int testId, TestRailResult data)
        {
            // create command
            var command = string.Format(ApiCommands.ADD_RESULT, testId);

            // execute command
            return ExecutePost<TestRailResult>(command, data);
        }

        /// <summary>
        /// Adds a new test result, comment or assigns a test (for a test run and case combination)
        /// </summary>
        /// <param name="runId">The ID of the test run</param>
        /// <param name="caseId">The ID of the test case</param>
        /// <param name="data">Request DTO</param>
        /// <returns>Success, the test results were created and are returned as part of the response</returns>
        /// <remarks>It's recommended to use add_results_for_cases instead if you plan to add results for multiple test cases.</remarks>
        public TestRailResult AddResultForCase(int runId, int caseId, TestRailResult data)
        {
            // create command
            var command = string.Format(ApiCommands.ADD_RESULT_FOR_CASE, runId, caseId);

            // execute command
            return ExecutePost<TestRailResult>(command, data);
        }

        /// <summary>
        /// Adds one or more new test results, comments or assigns one or more tests.
        /// Ideal for test automation to bulk-add multiple test results in one step.
        /// </summary>
        /// <param name="runId">The ID of the test run the results should be added to</param>
        /// <param name="data">Request DTO</param>
        /// <returns>Success, the test results were created and are returned as part of the response</returns>
        /// <remarks>Requires TestRail 3.1 or later.</remarks>
        public IEnumerable<TestRailResult> AddResults(int runId, params TestRailResult[] data)
        {
            // create command
            var command = string.Format(ApiCommands.ADD_RESULTS, runId);

            // compose content
            var content = new { Results = data };

            // execute command
            return ExecutePost<TestRailResult[]>(command, content);
        }

        /// <summary>
        /// Adds one or more new test results, comments or assigns one or more tests (using the case IDs).
        /// Ideal for test automation to bulk-add multiple test results in one step.
        /// </summary>
        /// <param name="runId">The ID of the test run the results should be added to</param>
        /// <param name="data">Request DTO</param>
        /// <returns>Success, the test results were created and are returned as part of the response</returns>
        public IEnumerable<TestRailResult> AddResultsForCases(int runId, params TestRailResult[] data)
        {
            // create command
            var command = string.Format(ApiCommands.ADD_RESULTS_FOR_CASES, runId);

            // compose content
            var content = new { Results = data };

            // execute command
            return ExecutePost<TestRailResult[]>(command, content);
        }
        #endregion
    }
}