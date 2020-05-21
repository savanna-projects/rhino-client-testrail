/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 * http://docs.gurock.com/testrail-api2/reference-sections
 */
using Automation.TestRail.Contracts;
using Automation.TestRail.Internal;
using System;
using System.Diagnostics;

namespace Automation.TestRail.Clients
{
    /// <summary>
    /// Use the following API methods to request details about sections and to create or modify
    /// sections. Sections are used to group and organize test cases in test suites.
    /// </summary>
    public class TestRailSectionsClient : TestRailClient
    {
        #region *** constructors     ***
        /// <summary>
        /// Creates a new instance of this client
        /// </summary>
        /// <param name="testRailServer">Test-Rail server to create a client against</param>
        /// <param name="user">Test-Rail user name (account email)</param>
        /// <param name="password">Test-Rail password</param>
        public TestRailSectionsClient(string testRailServer, string user, string password)
            : base(testRailServer, user, password) { }

        /// <summary>
        /// Creates a new instance of this client
        /// </summary>
        /// <param name="testRailServer">Test-Rail server to create a client against</param>
        /// <param name="user">Test-Rail user name (account email)</param>
        /// <param name="password">Test-Rail password</param>
        /// <param name="logger">Logger implementation for this client</param>
        public TestRailSectionsClient(Uri testRailServer, string user, string password, TraceListener logger)
            : base(testRailServer, user, password, logger) { }
        #endregion

        #region *** pipeline: get    ***
        /// <summary>
        /// Returns an existing section.
        /// </summary>
        /// <param name="sectionId">The ID of the section</param>
        /// <returns>Success, the section is returned as part of the response</returns>
        public TestRailSection GetSection(int sectionId)
        {
            // create command
            var command = string.Format(ApiCommands.GET_SECTION, sectionId);

            // execute command
            return ExecuteGet<TestRailSection>(command);
        }

        /// <summary>
        /// Returns a list of sections for a project and test suite.
        /// </summary>
        /// <param name="projectId">The ID of the project</param>
        /// <param name="suiteId">The ID of the test suite (optional if the project is operating in single suite mode)</param>
        /// <returns>Success, the sections are returned as part of the response</returns>
        public TestRailSection[] GetSections(int projectId, int suiteId)
        {
            // create command
            var command = string.Format(ApiCommands.GET_SECTIONS, projectId, suiteId);

            // execute command
            return ExecuteGet<TestRailSection[]>(command);
        }
        #endregion

        #region *** pipeline: add    ***
        /// <summary>
        /// Creates a new section.
        /// </summary>
        /// <param name="projectId">The ID of the project</param>
        /// <param name="data">Request DTO</param>
        /// <returns>Success, the section was created and is returned as part of the response</returns>
        public TestRailSection AddSection(int projectId, TestRailSection data) => Post(projectId, data);

        /// <summary>
        /// Creates a new section.
        /// </summary>
        /// <param name="project">The ID of the project</param>
        /// <param name="data">Request DTO</param>
        /// <returns>Success, the section was created and is returned as part of the response</returns>
        public TestRailSection AddSection(string project, TestRailSection data)
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

        private TestRailSection Post(int projectId, TestRailSection data)
        {
            // create command
            var command = string.Format(ApiCommands.ADD_SECTION, projectId);

            // execute command
            return ExecutePost<TestRailSection>(command, data);
        }
        #endregion

        #region *** pipeline: update ***
        /// <summary>
        /// Updates an existing section (partial updates are supported, i.e. you can submit and update
        /// specific fields only).
        /// </summary>
        /// <param name="sectionId">The ID of the section</param>
        /// <param name="data">Request DTO</param>
        /// <returns>Success, the section was updated and is returned as part of the response</returns>
        public TestRailSection UpdateSection(int sectionId, TestRailSection data)
        {
            // create command
            var command = string.Format(ApiCommands.UPDATE_SECTION, sectionId);

            // execute command
            return ExecutePost<TestRailSection>(command, data);
        }
        #endregion

        #region *** pipeline: delete ***
        /// <summary>
        /// Deletes an existing section.
        /// </summary>
        /// <param name="sectionId">The ID of the section</param>
        public void DeleteSection(int sectionId)
        {
            // create command
            var command = string.Format(ApiCommands.DELETE_SECTION, sectionId);

            // execute command
            ExecutePost(command);
        }
        #endregion
    }
}