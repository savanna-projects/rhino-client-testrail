/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 * http://docs.gurock.com/testrail-api2/reference-priorities
 */
using Automation.TestRail.Contracts;
using Automation.TestRail.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Automation.TestRail.Clients
{
    /// <summary>
    /// Use the following API methods to request details about priorities.
    /// </summary>
    public class TestRailPrioritiesClient : TestRailClient
    {
        #region *** constructors  ***
        /// <summary>
        /// Creates a new instance of this client
        /// </summary>
        /// <param name="testRailServer">Test-Rail server to create a client against</param>
        /// <param name="user">Test-Rail user name (account email)</param>
        /// <param name="password">Test-Rail password</param>
        public TestRailPrioritiesClient(string testRailServer, string user, string password)
            : base(testRailServer, user, password) { }

        /// <summary>
        /// Creates a new instance of this client
        /// </summary>
        /// <param name="testRailServer">Test-Rail server to create a client against</param>
        /// <param name="user">Test-Rail user name (account email)</param>
        /// <param name="password">Test-Rail password</param>
        /// <param name="logger">Logger implementation for this client</param>
        public TestRailPrioritiesClient(Uri testRailServer, string user, string password, TraceListener logger)
            : base(testRailServer, user, password, logger) { }
        #endregion

        #region *** pipeline: get ***
        /// <summary>
        /// Returns a list of available priorities.
        /// </summary>
        /// <returns>Success, the available priorities are returned as part of the response</returns>
        public IEnumerable<TestRailPriority> GetPriorities()
            => ExecuteGet<TestRailPriority[]>(ApiCommands.GET_PRIORITIES);
        #endregion
    }
}