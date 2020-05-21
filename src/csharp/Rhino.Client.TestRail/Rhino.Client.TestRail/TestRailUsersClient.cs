/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 * http://docs.gurock.com/testrail-api2/reference-users
 */
using Rhino.Client.TestRail.Contracts;
using Rhino.Client.TestRail.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Rhino.Client.TestRail
{
    /// <summary>
    /// Use the following API methods to request details about users.
    /// </summary>
    public class TestRailUsersClient : TestRailClient
    {
        #region *** constructors  ***
        /// <summary>
        /// Creates a new instance of this client
        /// </summary>
        /// <param name="testRailServer">Test-Rail server to create a client against</param>
        /// <param name="user">Test-Rail user name (account email)</param>
        /// <param name="password">Test-Rail password</param>
        public TestRailUsersClient(string testRailServer, string user, string password)
            : base(testRailServer, user, password) { }

        /// <summary>
        /// Creates a new instance of this client
        /// </summary>
        /// <param name="testRailServer">Test-Rail server to create a client against</param>
        /// <param name="user">Test-Rail user name (account email)</param>
        /// <param name="password">Test-Rail password</param>
        /// <param name="logger">Logger implementation for this client</param>
        public TestRailUsersClient(Uri testRailServer, string user, string password, TraceListener logger)
            : base(testRailServer, user, password, logger) { }
        #endregion

        #region *** pipeline: get ***
        /// <summary>
        /// Returns an existing user.
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <returns>Success, the user is returned as part of the response</returns>
        public TestRailUser GetUser(int userId)
        {
            // create command
            var command = string.Format(ApiCommands.GET_USER, userId);

            // execute command
            return ExecuteGet<TestRailUser>(command);
        }

        /// <summary>
        /// Returns an existing user by his/her email address.
        /// </summary>
        /// <param name="email">The email address to get the user for</param>
        /// <returns>Success, the user is returned as part of the response</returns>
        public TestRailUser GetUserByEmail(string email)
        {
            // create command
            var command = string.Format(ApiCommands.GET_USER_BY_EMAIL, email);

            // execute command
            return ExecuteGet<TestRailUser>(command);
        }

        /// <summary>
        /// Returns a list of users.
        /// </summary>
        /// <returns>Success, the users are returned as part of the response</returns>
        public IEnumerable<TestRailUser> GetUsers()
            => ExecuteGet<TestRailUser[]>(ApiCommands.GET_USERS);
        #endregion
    }
}