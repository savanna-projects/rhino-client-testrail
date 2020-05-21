/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 * http://docs.gurock.com/testrail-api2/reference-plans
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Automation.TestRail.Contracts;
using Automation.TestRail.Internal;

namespace Automation.TestRail.Clients
{
    /// <summary>
    /// Use the following API methods to request details about test plans and to create or modify test plans.
    /// </summary>
    public class TestRailPlansClient : TestRailClient
    {
        #region *** constructors     ***
        /// <summary>
        /// Creates a new instance of this client
        /// </summary>
        /// <param name="testRailServer">Test-Rail server to create a client against</param>
        /// <param name="user">Test-Rail user name (account email)</param>
        /// <param name="password">Test-Rail password</param>
        public TestRailPlansClient(string testRailServer, string user, string password)
            : base(testRailServer, user, password) { }

        /// <summary>
        /// Creates a new instance of this client
        /// </summary>
        /// <param name="testRailServer">Test-Rail server to create a client against</param>
        /// <param name="user">Test-Rail user name (account email)</param>
        /// <param name="password">Test-Rail password</param>
        /// <param name="logger">Logger implementation for this client</param>
        public TestRailPlansClient(Uri testRailServer, string user, string password, TraceListener logger)
            : base(testRailServer, user, password, logger) { }
        #endregion

        #region *** pipeline: get    ***
        /// <summary>
        /// Returns an existing test plan.
        /// </summary>
        /// <param name="planId">The ID of the test plan</param>
        /// <returns>Success, the test plan and its test runs are returned as part of the response</returns>
        public TestRailPlan GetPlan(int planId)
        {
            // create command
            var command = string.Format(ApiCommands.GET_PLAN, planId);

            // execute command
            return ExecuteGet<TestRailPlan>(command);
        }

        /// <summary>
        /// Returns a list of test plans for a project.
        /// </summary>
        /// <param name="projectId">The ID of the project</param>
        /// <returns>Success, the test plans are returned as part of the response</returns>
        /// <remarks>This method will return up to 250 entries in the response array. To retrieve additional 
        /// entries, you can make additional requests using the offset filter.</remarks>
        public IEnumerable<TestRailPlan> GetPlans(int projectId) => Get(projectId);

        /// <summary>
        /// Returns a list of test plans for a project.
        /// </summary>
        /// <param name="project">The name of the project</param>
        /// <returns>Success, the test plans are returned as part of the response</returns>
        /// <remarks>This method will return up to 250 entries in the response array. To retrieve additional 
        /// entries, you can make additional requests using the offset filter.</remarks>
        public IEnumerable<TestRailPlan> GetPlans(string project)
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
        /// Returns a list of test plans for a project.
        /// </summary>
        /// <param name="projectId">The ID of the project</param>
        /// <param name="offset">Use :offset to skip records</param>
        /// <returns>Success, the test plans are returned as part of the response</returns>
        public IEnumerable<TestRailPlan> GetPlans(int projectId, int offset) => Get(projectId, offset);

        /// <summary>
        /// Returns a list of test plans for a project.
        /// </summary>
        /// <param name="project">The name of the project</param>
        /// <param name="offset">Use :offset to skip records</param>
        /// <returns>Success, the test plans are returned as part of the response</returns>
        public IEnumerable<TestRailPlan> GetPlans(string project, int offset)
        {
            // get project id
            var testRailProject = GetProjectByName(project);
            if (testRailProject == default)
            {
                return default;
            }

            // return entity
            return Get(testRailProject.Id, offset);
        }

        private IEnumerable<TestRailPlan> Get(int projectId)
        {
            // create command
            var command = string.Format(ApiCommands.GET_PLANS, projectId);

            // execute command
            return ExecuteGet<TestRailPlan[]>(command);
        }

        private IEnumerable<TestRailPlan> Get(int projectId, int offset)
        {
            // create command
            var command = string.Format(ApiCommands.GET_PLANS_OFFSET, projectId, offset);

            // execute command
            return ExecuteGet<TestRailPlan[]>(command);
        }
        #endregion

        #region *** pipeline: add    ***
        /// <summary>
        /// Creates a new test plan.
        /// </summary>
        /// <param name="projectId">The ID of the project the test plan should be added to</param>
        /// <param name="data">Request DTO</param>
        /// <returns>Success, the test plan was created and is returned as part of the response</returns>
        public TestRailPlan AddPlan(int projectId, TestRailPlan data) => Post(projectId, data);

        /// <summary>
        /// Creates a new test plan.
        /// </summary>
        /// <param name="project">The name of the project the test plan should be added to</param>
        /// <param name="data">Request DTO</param>
        /// <returns>Success, the test plan was created and is returned as part of the response</returns>
        public TestRailPlan AddPlan(string project, TestRailPlan data)
        {
            // get project id
            var testRailProject = GetProjectByName(project);
            if (testRailProject == default)
            {
                return default;
            }

            // return entity
            return Post(testRailProject.Id, data);
        }

        /// <summary>
        /// Adds one or more new test runs to a test plan.
        /// </summary>
        /// <param name="planId">The ID of the plan the test runs should be added to</param>
        /// <param name="data">Request DTO</param>
        /// <returns>Success, the test run(s) were created and are returned as part of the response.</returns>
        /// <remarks>Please note that test runs in a plan are organized into 'entries' and these 
        /// have their own IDs (to be used with update_plan_entry and delete_plan_entry, respectively).</remarks>
        public TestRailPlanEntry AddPlanEntry(int planId, TestRailPlanEntry data)
        {
            // create command
            var command = string.Format(ApiCommands.ADD_PLAN_ENTRY, planId);

            // execute command
            return ExecutePost<TestRailPlanEntry>(command, data);
        }

        private TestRailPlan Post(int projectId, TestRailPlan data)
        {
            // create command
            var command = string.Format(ApiCommands.ADD_PLAN, projectId);

            // execute command
            return ExecutePost<TestRailPlan>(command, data);
        }
        #endregion

        #region *** pipeline: update ***
        /// <summary>
        /// Updates an existing test plan (partial updates are supported, i.e. 
        /// you can submit and update specific fields only).
        /// </summary>
        /// <param name="planId">The ID of the test plan</param>
        /// <param name="data">Request DTO</param>
        /// <returns>Success, the test plan was updated and is returned as part of the response</returns>
        public TestRailPlan UpdatePlan(int planId, TestRailPlan data)
        {
            // create command
            var command = string.Format(ApiCommands.UPDATE_PLAN, planId);

            // execute command
            return ExecutePost<TestRailPlan>(command, data);
        }

        /// <summary>
        /// Updates one or more existing test runs in a plan (partial updates are supported, i.e. 
        /// you can submit and update specific fields only).
        /// </summary>
        /// <param name="planId">The ID of the test plan</param>
        /// <param name="entryId">The ID of the test plan entry (note: not the test run ID)</param>
        /// <param name="data">Request DTO</param>
        /// <returns>Success, the test run(s) were updated and are returned as part of the response</returns>
        public TestRailPlanEntry UpdatePlanEntry(int planId, string entryId, TestRailPlanEntry data)
        {
            // create command
            var command = string.Format(ApiCommands.UPDATE_PLAN_ENTRY, planId, entryId);

            // execute command
            return ExecutePost<TestRailPlanEntry>(command, data);
        }
        #endregion

        #region *** pipeline: close  ***
        /// <summary>
        /// Closes an existing test plan and archives its test runs & results.
        /// </summary>
        /// <param name="planId">The ID of the test plan</param>
        /// <returns>Success, the test plan and all its test runs were closed (archived). 
        /// The test plan and its test runs are returned as part of the response.</returns>
        public TestRailPlan ClosePlan(int planId)
        {
            // create command
            var command = string.Format(ApiCommands.CLOSE_PLAN, planId);

            // execute command
            return ExecutePost<TestRailPlan>(command);
        }
        #endregion

        #region *** pipeline: delete ***
        /// <summary>
        /// Deletes an existing test plan.
        /// </summary>
        /// <param name="planId">The ID of the test plan</param>
        public void DeletePlan(int planId)
        {
            // create command
            var command = string.Format(ApiCommands.DELETE_PLAN, planId);

            // execute command
            ExecutePost(command);
        }

        /// <summary>
        /// Deletes one or more existing test runs from a plan.
        /// </summary>
        /// <param name="planId">The ID of the test plan</param>
        /// <param name="entryId">The ID of the test plan entry (note: not the test run ID)</param>
        public void DeletePlanEntry(int planId, string entryId)
        {
            // create command
            var command = string.Format(ApiCommands.DELETE_PLAN_ENTRY, planId, entryId);

            // execute command
            ExecutePost(command);
        }
        #endregion
    }
}