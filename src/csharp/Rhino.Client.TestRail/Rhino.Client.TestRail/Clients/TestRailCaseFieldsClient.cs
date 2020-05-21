/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 * http://docs.gurock.com/testrail-api2/reference-cases-fields
 */
using Rhino.Client.TestRail.Contracts;
using Rhino.Client.TestRail.Internal;
using System;
using System.Diagnostics;

namespace Rhino.Client.TestRail.Clients
{
    /// <summary>
    /// Use the following API methods to request details about custom fields for test cases.
    /// </summary>
    public class TestRailCaseFieldsClient : TestRailClient
    {
        #region *** constructors     ***
        public TestRailCaseFieldsClient(string testRailServer, string user, string password)
            : base(testRailServer, user, password) { }

        public TestRailCaseFieldsClient(Uri testRailServer, string user, string password, TraceListener logger)
            : base(testRailServer, user, password, logger) { }
        #endregion

        #region *** pipeline: get    ***
        /// <summary>
        /// Returns a list of available test case custom fields.
        /// </summary>
        /// <returns>Success, the available custom fields are returned as part of the response</returns>
        public TestRailCaseField[] GetCaseFields()
            => ExecuteGet<TestRailCaseField[]>(ApiCommands.GET_CASE_FIELDS);
        #endregion

        #region *** pipeline: add    ***
        /// <summary>
        /// Creates a new test case custom field.
        /// </summary>
        /// <param name="data">Request DTO</param>
        /// <returns>Success, the new custom field is returned in the response</returns>
        public TestRailCaseField AddCaseField(TestRailCaseField data)
            => ExecutePost<TestRailCaseField>(ApiCommands.ADD_CASE_FIELD, data);
        #endregion

        #region *** pipeline: delete ***
        /// <summary>
        /// Delete custom fields (uses administrator API)
        /// </summary>
        /// <param name="customFieldId">Custom filed id (field to delete)</param>
        public void DeleteCustomField(int customFieldId)
            => ExecutePost(string.Format(ApiCommands.DELETE_CASE_FIELD, customFieldId));
        #endregion
    }
}