/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 * http://docs.gurock.com/testrail-api2/reference-configs
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
    /// Use the following API methods to request details about configurations and to create or modify configurations.
    /// </summary>
    public class TestRailConfigurationsClient : TestRailClient
    {
        #region *** constructors     ***
        /// <summary>
        /// Creates a new instance of this client
        /// </summary>
        /// <param name="testRailServer">Test-Rail server to create a client against</param>
        /// <param name="user">Test-Rail user name (account email)</param>
        /// <param name="password">Test-Rail password</param>
        public TestRailConfigurationsClient(string testRailServer, string user, string password)
            : base(testRailServer, user, password) { }

        /// <summary>
        /// Creates a new instance of this client
        /// </summary>
        /// <param name="testRailServer">Test-Rail server to create a client against</param>
        /// <param name="user">Test-Rail user name (account email)</param>
        /// <param name="password">Test-Rail password</param>
        /// <param name="logger">Logger implementation for this client</param>
        public TestRailConfigurationsClient(Uri testRailServer, string user, string password, TraceListener logger)
            : base(testRailServer, user, password, logger) { }
        #endregion

        #region *** pipeline: get    ***
        /// <summary>
        /// Returns a list of available configurations, grouped by configuration 
        /// groups (requires TestRail 3.1 or later).
        /// </summary>
        /// <param name="projectId">The ID of the project</param>
        /// <returns>The response includes an array of configuration groups, each with a list of configurations.</returns>
        public IEnumerable<TestRailConfiguration> GetConfigs(int projectId) => Get(projectId);

        /// <summary>
        /// Returns a list of available configurations, grouped by configuration 
        /// groups (requires TestRail 3.1 or later).
        /// </summary>
        /// <param name="project">The name of the project</param>
        /// <returns>The response includes an array of configuration groups, each with a list of configurations.</returns>
        public IEnumerable<TestRailConfiguration> GetConfigs(string project)
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

        private IEnumerable<TestRailConfiguration> Get(int projectId)
        {
            // create command
            var command = string.Format(ApiCommands.GET_CONFIGS, projectId);

            // execute command
            return ExecuteGet<TestRailConfiguration[]>(command);
        }
        #endregion

        #region *** pipeline: add    ***
        /// <summary>
        /// Creates a new configuration group (requires TestRail 5.2 or later).
        /// </summary>
        /// <param name="projectId">The ID of the project the configuration group should be added to</param>
        /// <param name="data">Request DTO</param>
        /// <returns>Success, the configuration group was created and is returned as part of the response</returns>
        public TestRailConfigurationGroup AddConfigGroup(int projectId, TestRailConfigurationGroup data)
        {
            // create command
            var command = string.Format(ApiCommands.ADD_CONFIG_GROUP, projectId);

            // execute command
            return ExecutePost<TestRailConfigurationGroup>(command, data);
        }

        /// <summary>
        /// Creates a new configuration (requires TestRail 5.2 or later).
        /// </summary>
        /// <param name="configGroupId">The ID of the configuration group the configuration should be added to</param>
        /// <param name="data">Request DTO</param>
        /// <returns>Success, the configuration was created and is returned as part of the response</returns>
        public TestRailConfiguration AddConfig(int configGroupId, TestRailConfiguration data) => Post(configGroupId, data);

        /// <summary>
        /// Creates a new configuration (requires TestRail 5.2 or later).
        /// </summary>
        /// <param name="projectId">The ID of the project the configuration group should be added to</param>
        /// <param name="configGroup">The name of the configuration group the configuration should be added to</param>
        /// <param name="data">Request DTO</param>
        /// <returns>Success, the configuration was created and is returned as part of the response</returns>
        public TestRailConfiguration AddConfig(int projectId, string configGroup, TestRailConfiguration data)
        {
            // get configuration group
            var testConfigurations = Get(projectId);
            var group = testConfigurations.FirstOrDefault(i => i.Name.Equals(configGroup, COMPARE));
            if (group == default)
            {
                return default;
            }

            // execute command
            return Post(group.Id, data);
        }

        private TestRailConfiguration Post(int configGroupId, TestRailConfiguration data)
        {
            // create command
            var command = string.Format(ApiCommands.ADD_CONFIG, configGroupId);

            // execute command
            return ExecutePost<TestRailConfiguration>(command, data);
        }
        #endregion

        #region *** pipeline: update ***
        /// <summary>
        /// Updates an existing configuration group (requires TestRail 5.2 or later).
        /// </summary>
        /// <param name="configGroupId">The ID of the configuration group</param>
        /// <param name="data">Request DTO</param>
        /// <returns>Success, the configuration group was updated and is returned as part of the response</returns>
        public TestRailConfigurationGroup UpdateConfigGroup(int configGroupId, TestRailConfigurationGroup data)
        {
            // create command
            var command = string.Format(ApiCommands.UPDATE_CONFIG_GROUP, configGroupId);

            // execute command
            return ExecutePost<TestRailConfigurationGroup>(command, data);
        }

        /// <summary>
        /// Updates an existing configuration (requires TestRail 5.2 or later).
        /// </summary>
        /// <param name="configId">The ID of the configuration</param>
        /// <param name="data">Request DTO</param>
        /// <returns>Success, the configuration was updated and is returned as part of the response</returns>
        public TestRailConfiguration UpdateConfig(int configId, TestRailConfiguration data)
        {
            // create command
            var command = string.Format(ApiCommands.UPDATE_CONFIG, configId);

            // execute command
            return ExecutePost<TestRailConfiguration>(command, data);
        }
        #endregion

        #region *** pipeline: delete ***
        /// <summary>
        /// Deletes an existing configuration group and its configurations (requires TestRail 5.2 or later).
        /// </summary>
        /// <param name="configGroupId">The ID of the configuration group</param>
        public void DeleteConfigGroup(int configGroupId)
        {
            // create command
            var command = string.Format(ApiCommands.DELETE_CONFIG_GROUP, configGroupId);

            // execute command
            ExecutePost(command);
        }

        /// <summary>
        /// Deletes an existing configuration (requires TestRail 5.2 or later).
        /// </summary>
        /// <param name="configId">The ID of the configuration</param>
        public void DeleteConfig(int configId)
        {
            // create command
            var command = string.Format(ApiCommands.DELETE_CONFIG, configId);

            // execute command
            ExecutePost(command);
        }
        #endregion
    }
}