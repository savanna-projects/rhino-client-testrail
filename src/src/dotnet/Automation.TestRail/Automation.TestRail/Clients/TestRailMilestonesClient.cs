/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 * http://docs.gurock.com/testrail-api2/reference-milestones
 */
using Automation.TestRail.Contracts;
using Automation.TestRail.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Automation.TestRail.Clients
{
    /// <summary>
    /// Use the following API methods to request details about milestones and to create or modify milestones.
    /// </summary>
    public class TestRailMilestonesClient : TestRailClient
    {
        #region *** constructors     ***
        /// <summary>
        /// Creates a new instance of this client
        /// </summary>
        /// <param name="testRailServer">Test-Rail server to create a client against</param>
        /// <param name="user">Test-Rail user name (account email)</param>
        /// <param name="password">Test-Rail password</param>
        public TestRailMilestonesClient(string testRailServer, string user, string password)
            : base(testRailServer, user, password) { }

        /// <summary>
        /// Creates a new instance of this client
        /// </summary>
        /// <param name="testRailServer">Test-Rail server to create a client against</param>
        /// <param name="user">Test-Rail user name (account email)</param>
        /// <param name="password">Test-Rail password</param>
        /// <param name="logger">Logger implementation for this client</param>
        public TestRailMilestonesClient(Uri testRailServer, string user, string password, TraceListener logger)
            : base(testRailServer, user, password, logger) { }
        #endregion

        #region *** pipeline: get    ***
        /// <summary>
        /// Returns an existing milestone.
        /// </summary>
        /// <param name="milestoneId">The ID of the milestone</param>
        /// <returns>Success, the milestone is returned as part of the response</returns>
        public TestRailMileStone GetMileStone(int milestoneId)
        {
            // create command
            var command = string.Format(ApiCommands.GET_MILESTONE, milestoneId);

            // execute command
            return ExecuteGet<TestRailMileStone>(command);
        }

        /// <summary>
        /// Returns the list of milestones for a project.
        /// </summary>
        /// <param name="projectId">The ID of the project</param>
        /// <returns>Success, the milestones are returned as part of the response</returns>
        public IEnumerable<TestRailMileStone> GetMileStones(int projectId) => Get(projectId);

        /// <summary>
        /// Returns the list of milestones for a project.
        /// </summary>
        /// <param name="project">The ID of the project</param>
        /// <returns>Success, the milestones are returned as part of the response</returns>
        public IEnumerable<TestRailMileStone> GetMileStones(string project)
        {
            // get project id
            var testRailProject = GetProjectByName(project);
            if (testRailProject == default)
            {
                return default;
            }

            // execute command
            return Get(testRailProject.Id);
        }

        private IEnumerable<TestRailMileStone> Get(int projectId)
        {
            // create command
            var command = string.Format(ApiCommands.GET_MILESTONES, projectId);

            // execute command
            return ExecuteGet<TestRailMileStone[]>(command);
        }
        #endregion

        #region *** pipeline: add    ***
        /// <summary>
        /// Creates a new milestone.
        /// </summary>
        /// <param name="projectId">The ID of the project the milestone should be added to</param>
        /// <param name="data">Request DTO</param>
        /// <returns>Success, the milestone was created and is returned as part of the response</returns>
        public TestRailMileStone AddMilestone(int projectId, TestRailMileStone data)
        {
            // create command
            var command = string.Format(ApiCommands.ADD_MILESTONE, projectId);

            // execute command
            return ExecutePost<TestRailMileStone>(command, data);
        }
        #endregion

        #region *** pipeline: update ***
        /// <summary>
        /// Updates an existing milestone (partial updates are supported, i.e. you can submit and update specific fields only).
        /// </summary>
        /// <param name="milestoneId">The ID of the milestone</param>
        /// <param name="data">Request DTO</param>
        /// <returns>Success, the milestone was updated and is returned as part of the response</returns>
        public TestRailMileStone UpdateMilestone(int milestoneId, TestRailMileStone data)
        {
            // create command
            var command = string.Format(ApiCommands.UPDATE_MILESTONE, milestoneId);

            // execute command
            return ExecutePost<TestRailMileStone>(command, data);
        }
        #endregion

        #region *** pipeline: delete ***
        /// <summary>
        /// Deletes an existing milestone.
        /// </summary>
        /// <param name="milestoneId">The ID of the milestone</param>
        /// <returns>Success, the milestone was deleted</returns>
        public void DeleteMilestone(int milestoneId)
        {
            // create command
            var command = string.Format(ApiCommands.DELETE_MILESTONE, milestoneId);

            // execute command
            ExecutePost(command);
        }
        #endregion
    }
}
