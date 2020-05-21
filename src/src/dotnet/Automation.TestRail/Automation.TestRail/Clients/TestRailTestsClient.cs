/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 * http://docs.gurock.com/testrail-api2/reference-tests
 */
using Automation.TestRail.Contracts;
using Automation.TestRail.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Automation.TestRail.Clients
{
    /// <summary>
    /// Use the following API methods to request details about tests.
    /// </summary>
    public class TestRailTestsClient : TestRailClient
    {
        #region *** constructors  ***
        /// <summary>
        /// Creates a new instance of this client
        /// </summary>
        /// <param name="testRailServer">Test-Rail server to create a client against</param>
        /// <param name="user">Test-Rail user name (account email)</param>
        /// <param name="password">Test-Rail password</param>
        public TestRailTestsClient(string testRailServer, string user, string password)
            : base(testRailServer, user, password) { }

        /// <summary>
        /// Creates a new instance of this client
        /// </summary>
        /// <param name="testRailServer">Test-Rail server to create a client against</param>
        /// <param name="user">Test-Rail user name (account email)</param>
        /// <param name="password">Test-Rail password</param>
        /// <param name="logger">Logger implementation for this client</param>
        public TestRailTestsClient(Uri testRailServer, string user, string password, TraceListener logger)
            : base(testRailServer, user, password, logger) { }
        #endregion

        #region *** pipeline: get ***
        /// <summary>
        /// Returns an existing test.
        /// </summary>
        /// <param name="testId">The ID of the test</param>
        /// <returns>Success, the test is returned as part of the response</returns>
        public TestRailTest GetTest(int testId)
        {
            // create command
            var command = string.Format(ApiCommands.GET_TEST, testId);

            // execute command
            return ExecuteGet<TestRailTest>(command);
        }

        /// <summary>
        /// Returns a list of tests for a test run.
        /// </summary>
        /// <param name="runId">The ID of the test run</param>
        /// <returns>Success, the tests are returned as part of the response</returns>
        public IEnumerable<TestRailTest> GetTests(int runId)
        {
            // create command
            var command = string.Format(ApiCommands.GET_TESTS, runId);

            // execute command
            return ExecuteGet<TestRailTest[]>(command);
        }
        #endregion
    }
}