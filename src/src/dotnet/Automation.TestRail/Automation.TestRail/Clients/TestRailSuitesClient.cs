/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 * http://docs.gurock.com/testrail-api2/reference-suites
 */
using Automation.TestRail.Contracts;
using Automation.TestRail.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Automation.TestRail.Clients
{
    /// <summary>
    /// Use the following API methods to request details about test suites and to create or modify test suites.
    /// </summary>
    public class TestRailSuitesClient : TestRailClient
    {
        #region *** constructors     ***
        /// <summary>
        /// Creates a new instance of this client
        /// </summary>
        /// <param name="testRailServer">Test-Rail server to create a client against</param>
        /// <param name="user">Test-Rail user name (account email)</param>
        /// <param name="password">Test-Rail password</param>
        public TestRailSuitesClient(string testRailServer, string user, string password)
            : base(testRailServer, user, password) { }

        /// <summary>
        /// Creates a new instance of this client
        /// </summary>
        /// <param name="testRailServer">Test-Rail server to create a client against</param>
        /// <param name="user">Test-Rail user name (account email)</param>
        /// <param name="password">Test-Rail password</param>
        /// <param name="logger">Logger implementation for this client</param>
        public TestRailSuitesClient(Uri testRailServer, string user, string password, TraceListener logger)
            : base(testRailServer, user, password, logger) { }
        #endregion

        #region *** pipeline: get    ***
        /// <summary>
        /// Returns an existing test suite.
        /// </summary>
        /// <param name="suiteId">The ID of the test suite</param>
        /// <returns>Success, the test suite is returned as part of the response</returns>
        public TestRailSuite GetSuite(int suiteId)
        {
            // create command
            var command = string.Format(ApiCommands.GET_SUITE, suiteId);

            // execute command
            return ExecuteGet<TestRailSuite>(command);
        }

        /// <summary>
        /// Returns a list of test suites for a project.
        /// </summary>
        /// <param name="projectId">The ID of the project</param>
        /// <returns>Success, the test suites are returned as part of the response</returns>
        public IEnumerable<TestRailSuite> GetSuites(int projectId) => Get(projectId);

        /// <summary>
        /// Returns a list of test suites for a project.
        /// </summary>
        /// <param name="project">The name of the project</param>
        /// <returns>Success, the test suites are returned as part of the response</returns>
        public IEnumerable<TestRailSuite> GetSuites(string project)
        {
            // get project id
            var testRailProject = GetProjectByName(project);
            if (testRailProject == default)
            {
                return default;
            }

            // return entity
            return Get(testRailProject.Id);
        }

        private IEnumerable<TestRailSuite> Get(int projectId)
        {
            // create command
            var command = string.Format(ApiCommands.GET_SUITES, projectId);

            // execute command
            return ExecuteGet<TestRailSuite[]>(command);
        }
        #endregion

        #region *** pipeline: add    ***
        /// <summary>
        /// Creates a new test suite.
        /// </summary>
        /// <param name="projectId">The ID of the project the test suite should be added to</param>
        /// <param name="data">Request DTO</param>
        /// <returns>Success, the test suite was created and is returned as part of the response</returns>
        public TestRailSuite AddSuite(int projectId, TestRailSuite data)
        {
            // create command
            var command = string.Format(ApiCommands.ADD_SUITE, projectId);

            // execute command
            return ExecutePost<TestRailSuite>(command, data);
        }
        #endregion

        #region *** pipeline: update ***
        /// <summary>
        /// Updates an existing test suite (partial updates are supported, i.e. 
        /// you can submit and update specific fields only).
        /// </summary>
        /// <param name="suiteId">The ID of the test suite</param>
        /// <param name="data">Request DTO</param>
        /// <returns>Success, the test suite was updated and is returned as part of the response</returns>
        public TestRailSuite UpdateSuite(int suiteId, TestRailSuite data)
        {
            // create command
            var command = string.Format(ApiCommands.UPDATE_SUITE, suiteId);

            // execute command
            return ExecutePost<TestRailSuite>(command, data);
        }
        #endregion

        #region *** pipeline: delete ***
        /// <summary>
        /// Deletes an existing test suite.
        /// </summary>
        /// <param name="suiteId">The ID of the test suite</param>
        public void DeleteSuite(int suiteId)
        {
            // create command
            var command = string.Format(ApiCommands.DELETE_SUITE, suiteId);

            // execute command
            ExecutePost(command);
        }
        #endregion
    }
}