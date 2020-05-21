/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 * http://docs.gurock.com/testrail-api2/reference-runs
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
    /// Use the following API methods to request details about test runs and to create or modify test runs.
    /// </summary>
    public class TestRailRunsClient : TestRailClient
    {
        #region *** constructors     ***
        /// <summary>
        /// Creates a new instance of this client
        /// </summary>
        /// <param name="testRailServer">Test-Rail server to create a client against</param>
        /// <param name="user">Test-Rail user name (account email)</param>
        /// <param name="password">Test-Rail password</param>
        public TestRailRunsClient(string testRailServer, string user, string password)
            : base(testRailServer, user, password) { }

        /// <summary>
        /// Creates a new instance of this client
        /// </summary>
        /// <param name="testRailServer">Test-Rail server to create a client against</param>
        /// <param name="user">Test-Rail user name (account email)</param>
        /// <param name="password">Test-Rail password</param>
        /// <param name="logger">Logger implementation for this client</param>
        public TestRailRunsClient(Uri testRailServer, string user, string password, TraceListener logger)
            : base(testRailServer, user, password, logger) { }
        #endregion

        #region *** pipeline: get    ***
        /// <summary>
        /// Returns an existing test run. Please see get_tests for the list of included tests in this run.
        /// </summary>
        /// <param name="runId">The ID of the test run</param>
        /// <returns>Success, the test run is returned as part of the response</returns>
        public TestRailRun GetRun(int runId)
        {
            // create command
            var command = string.Format(ApiCommands.GET_RUN, runId);

            // execute command
            return ExecuteGet<TestRailRun>(command);
        }

        /// <summary>
        /// Returns a list of test runs for a project.
        /// Only returns those test runs that are not part of a test plan (please see get_plans/get_plan for this).
        /// </summary>
        /// <param name="projectId">The ID of the project</param>
        /// <returns>Success, the test runs are returned as part of the response</returns>
        public IEnumerable<TestRailRun> GetRuns(int projectId) => Get(projectId);

        /// <summary>
        /// Returns a list of test runs for a project.
        /// Only returns those test runs that are not part of a test plan (please see get_plans/get_plan for this).
        /// </summary>
        /// <param name="project">The name of the project</param>
        /// <returns>Success, the test runs are returned as part of the response</returns>
        public IEnumerable<TestRailRun> GetPlans(string project)
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

        /// <summary>
        /// Returns a list of test runs for a project.
        /// Only returns those test runs that are not part of a test plan (please see get_plans/get_plan for this).
        /// </summary>
        /// <param name="projectId">The ID of the project</param>
        /// <param name="offset">Use :offset to skip records.</param>
        /// <returns>Success, the test runs are returned as part of the response</returns>
        public IEnumerable<TestRailRun> GetRuns(int projectId, int offset)
        {
            // create command
            var command = string.Format(ApiCommands.GET_RUNS_OFFSET, projectId, offset);

            // execute command
            return ExecuteGet<TestRailRun[]>(command);
        }

        /// <summary>
        /// Returns a list of test runs for a project.
        /// Only returns those test runs that are not part of a test plan (please see get_plans/get_plan for this).
        /// </summary>
        /// <param name="projectId">The ID of the project</param>
        /// <param name="suiteId">A comma-separated list of test suite IDs to filter by</param>
        /// <returns>Success, the test runs are returned as part of the response</returns>
        public IEnumerable<TestRailRun> GetRuns(int projectId, IEnumerable<int> suiteId)
        {
            // create command
            var suites = string.Join(",", suiteId.ToArray());
            var command = string.Format(ApiCommands.GET_RUNS_SUITE_ID, projectId, suites);

            // execute command
            return ExecuteGet<TestRailRun[]>(command);
        }

        private IEnumerable<TestRailRun> Get(int projectId)
        {
            // create command
            var command = string.Format(ApiCommands.GET_RUNS, projectId);

            // execute command
            return ExecuteGet<TestRailRun[]>(command);
        }
        #endregion

        #region *** pipeline: add    ***
        /// <summary>
        /// Creates a new test run.
        /// </summary>
        /// <param name="projectId">The ID of the project the test run should be added to</param>
        /// <param name="data">Request DTO</param>
        /// <returns>Success, the test run was created and is returned as part of the response</returns>
        public TestRailRun AddRun(int projectId, TestRailRun data)
        {
            // create command
            var command = string.Format(ApiCommands.ADD_RUN, projectId);

            // execute command
            return ExecutePost<TestRailRun>(command, data);
        }
        #endregion

        #region *** pipeline: update ***
        /// <summary>
        /// Updates an existing test run (partial updates are supported, i.e. 
        /// you can submit and update specific fields only).
        /// </summary>
        /// <param name="runId">The ID of the test run</param>
        /// <param name="data">Request DTO</param>
        /// <returns>Success, the test run was updated and is returned as part of the response</returns>
        public TestRailRun UpdateRun(int runId, TestRailRun data)
        {
            // create command
            var command = string.Format(ApiCommands.UPDATE_RUN, runId);

            // execute command
            return ExecutePost<TestRailRun>(command, data);
        }
        #endregion

        #region *** pipeline: close  ***
        /// <summary>
        /// Closes an existing test run and archives its tests & results.
        /// </summary>
        /// <param name="runId">The ID of the test run</param>
        /// <returns>Success, the test run was closed (archived) and is returned as part of the response.</returns>
        public TestRailRun CloseRun(int runId)
        {
            // create command
            var command = string.Format(ApiCommands.CLOSE_RUN, runId);

            // execute command
            return ExecutePost<TestRailRun>(command);
        }
        #endregion

        #region *** pipeline: delete ***
        /// <summary>
        /// Deletes an existing test run.
        /// </summary>
        /// <param name="runId">The ID of the test run</param>
        public void DeleteRun(int runId)
        {
            // create command
            var command = string.Format(ApiCommands.DELETE_RUN, runId);

            // execute command
            ExecutePost(command);
        }
        #endregion
    }
}