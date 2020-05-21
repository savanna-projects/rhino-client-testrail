/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 */
using System;
using System.Diagnostics;

namespace Automation.TestRail.Clients
{
    /// <summary>
    /// Generic factory for creating Test-Rail clients
    /// </summary>
    public class TestRailClientFactory
    {
        // members: state
        private readonly string testRailServer;
        private readonly string user;
        private readonly string password;

        #region *** constructors    ***
        /// <summary>
        /// Creates a new instance of this client
        /// </summary>
        /// <param name="testRailServer">Test-Rail server to create a client against</param>
        /// <param name="user">Test-Rail user name (account email)</param>
        /// <param name="password">Test-Rail password</param>
        public TestRailClientFactory(string testRailServer, string user, string password)
        {
            this.testRailServer = testRailServer;
            this.user = user;
            this.password = password;
        }
        #endregion

        /// <summary>
        /// Creates a new test-rail client instance
        /// </summary>
        /// <typeparam name="T">Type of client to generate</typeparam>
        /// <returns>ALM client of T type</returns>
        public T CreateClient<T>() where T : TestRailClient
        {
            // generate arguments
            var type = typeof(T);
            var args = new object[] { testRailServer, user, password };

            // create client
            var client = (T)Activator.CreateInstance(type, args);
            Trace.TraceInformation($"client [{typeof(T).FullName}] successfully created");

            // return client
            return client;
        }
    }
}