/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 * http://docs.gurock.com/testrail-api2/reference-projects
 */
using Automation.TestRail.Contracts;
using Automation.TestRail.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Automation.TestRail.Clients
{
    /// <summary>
    /// Use the following API methods to request details about projects and to create or modify projects.
    /// </summary>
    public class TestRailProjectsClient : TestRailClient
    {
        #region *** constructors     ***
        /// <summary>
        /// Creates a new instance of this client
        /// </summary>
        /// <param name="testRailServer">Test-Rail server to create a client against</param>
        /// <param name="user">Test-Rail user name (account email)</param>
        /// <param name="password">Test-Rail password</param>
        public TestRailProjectsClient(string testRailServer, string user, string password)
            : base(testRailServer, user, password) { }

        /// <summary>
        /// Creates a new instance of this client
        /// </summary>
        /// <param name="testRailServer">Test-Rail server to create a client against</param>
        /// <param name="user">Test-Rail user name (account email)</param>
        /// <param name="password">Test-Rail password</param>
        /// <param name="logger">Logger implementation for this client</param>
        public TestRailProjectsClient(Uri testRailServer, string user, string password, TraceListener logger)
            : base(testRailServer, user, password, logger) { }
        #endregion

        #region *** pipeline: get    ***
        /// <summary>
        /// Returns an existing project.
        /// </summary>
        /// <param name="projectId">The ID of the project</param>
        /// <returns>Success, the project is returned as part of the response</returns>
        public TestRailProject GetProject(int projectId) => Get(projectId);

        /// <summary>
        /// Returns an existing project.
        /// </summary>
        /// <param name="project">The name of the project</param>
        /// <returns>Success, the project is returned as part of the response</returns>
        public TestRailProject GetProject(string project)
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
        /// Returns the list of available projects.
        /// </summary>
        /// <returns>Success, the projects are returned as part of the response.</returns>
        /// <remarks>Only returns those projects with at least read-access.</remarks>
        public IEnumerable<TestRailProject> GetProjects()
            => ExecuteGet<TestRailProject[]>(ApiCommands.GET_PROJECTS);

        private TestRailProject Get(int projectId)
        {
            // create command
            var command = string.Format(ApiCommands.GET_PROJECT, projectId);

            // execute command
            return ExecuteGet<TestRailProject>(command);
        }
        #endregion

        #region *** pipeline: add    ***
        /// <summary>
        /// Creates a new project (administrator status required).
        /// </summary>
        /// <param name="data">Request DTO</param>
        /// <returns>Success, the project was created and is returned as part of the response</returns>
        public TestRailProject AddProject(TestRailProject data)
            => ExecutePost<TestRailProject>(ApiCommands.ADD_PROJECT, data);
        #endregion

        #region *** pipeline: update ***
        /// <summary>
        /// Updates an existing project (administrator status required; partial updates are supported, i.e. 
        /// you can submit and update specific fields only).
        /// </summary>
        /// <param name="projectId">The ID of the project</param>
        /// <param name="data">Request DTO</param>
        /// <returns>Success, the project was updated and is returned as part of the response</returns>
        public TestRailProject UpdateProject(int projectId, TestRailProject data)
        {
            // create command
            var command = string.Format(ApiCommands.UPDATE_PROJECT, projectId);

            // execute command
            return ExecutePost<TestRailProject>(command, data);
        }
        #endregion

        #region *** pipeline: delete ***
        /// <summary>
        /// Deletes an existing project (administrator status required).
        /// </summary>
        /// <param name="projectId">The ID of the project</param>
        public void DeleteProject(int projectId)
        {
            // create command
            var command = string.Format(ApiCommands.DELETE_PROJECT, projectId);

            // execute command
            ExecutePost(command);
        }
        #endregion
    }
}