/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 * http://docs.gurock.com/testrail-api2/reference-cases
 */
using Automation.TestRail.Contracts;
using Automation.TestRail.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Automation.TestRail.Clients
{
    /// <summary>
    /// Use the following API methods to request details about test cases and to create or modify test cases.
    /// </summary>
    public class TestRailCasesClient : TestRailClient
    {
        #region *** constructors     ***
        /// <summary>
        /// Creates a new instance of this client
        /// </summary>
        /// <param name="testRailServer">Test-Rail server to create a client against</param>
        /// <param name="user">Test-Rail user name (account email)</param>
        /// <param name="password">Test-Rail password</param>
        public TestRailCasesClient(string testRailServer, string user, string password)
            : base(testRailServer, user, password) { }

        /// <summary>
        /// Creates a new instance of this client
        /// </summary>
        /// <param name="testRailServer">Test-Rail server to create a client against</param>
        /// <param name="user">Test-Rail user name (account email)</param>
        /// <param name="password">Test-Rail password</param>
        /// <param name="logger">Logger implementation for this client</param>
        public TestRailCasesClient(Uri testRailServer, string user, string password, TraceListener logger)
            : base(testRailServer, user, password, logger) { }
        #endregion

        #region *** pipeline: get    ***
        /// <summary>
        /// Returns an existing test case.
        /// </summary>
        /// <param name="caseId">The ID of the test case</param>
        /// <returns>An existing test case</returns>
        public TestRailCase GetCase(int caseId)
        {
            return ExecuteGet<TestRailCase>(string.Format(ApiCommands.GET_CASE, caseId));
        }

        /// <summary>
        /// Returns a list of test cases for a test suite or specific section in a test suite.
        /// </summary>
        /// <param name="dto">Query-string information for getting test-cases</param>
        /// <returns>A list of test cases for a test suite</returns>
        public IEnumerable<TestRailCase> GetCases(GetCasesArguments dto)
        {
            // create command
            var command = dto.RoutingFactory();

            // execute command
            return ExecuteGet<TestRailCase[]>(command);
        }
        #endregion

        #region *** pipeline: add    ***
        /// <summary>
        /// Creates a new test case.
        /// </summary>
        /// <param name="sectionId">The ID of the section the test case should be added to</param>
        /// <param name="data">Request DTO</param>
        /// <returns>If successful, this method returns the new test case using the same response format as get_case</returns>
        public TestRailCase AddCase(int sectionId, TestRailCase data)
        {
            // create command
            var command = string.Format(ApiCommands.ADD_CASE, sectionId);

            // execute command
            return ExecutePost<TestRailCase>(command, data);
        }
        #endregion

        #region *** pipeline: update ***
        /// <summary>
        /// Updates an existing test case (partial updates are supported, 
        /// i.e. you can submit and update specific fields only).
        /// </summary>
        /// <param name="caseId">The ID of the test case</param>
        /// <param name="data">Request DTO</param>
        /// <returns>If successful, this method returns the updated test case using the same response format as get_case.</returns>
        public TestRailCase UpdateCase(int caseId, TestRailCase data)
        {
            // create command
            var command = string.Format(ApiCommands.UPDATE_CASE, caseId);

            // execute command
            return ExecutePost<TestRailCase>(command, data);
        }
        #endregion

        #region *** pipeline: delete ***
        /// <summary>
        /// Deletes an existing test case.
        /// </summary>
        /// <param name="caseId">The ID of the test case</param>
        public void DeleteCase(int caseId)
        {
            // create command
            var command = string.Format(ApiCommands.DELETE_CASE, caseId);

            // execute command
            ExecutePost(command);
        }
        #endregion
    }
}