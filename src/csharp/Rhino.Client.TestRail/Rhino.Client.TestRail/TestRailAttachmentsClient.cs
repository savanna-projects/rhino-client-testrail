/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 * http://docs.gurock.com/testrail-api2/reference-attachments
 */
using Rhino.Client.TestRail.Contracts;
using Rhino.Client.TestRail.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Rhino.Client.TestRail
{
    /// <summary>
    /// Use the following API methods to upload, retrieve and delete attachments.
    /// </summary>
    public class TestRailAttachmentsClient : TestRailClient
    {
        #region *** constructors     ***
        /// <summary>
        /// Creates a new instance of this client
        /// </summary>
        /// <param name="testRailServer">Test-Rail server to create a client against</param>
        /// <param name="user">Test-Rail user name (account email)</param>
        /// <param name="password">Test-Rail password</param>
        public TestRailAttachmentsClient(string testRailServer, string user, string password)
            : base(testRailServer, user, password) { }

        /// <summary>
        /// Creates a new instance of this client
        /// </summary>
        /// <param name="testRailServer">Test-Rail server to create a client against</param>
        /// <param name="user">Test-Rail user name (account email)</param>
        /// <param name="password">Test-Rail password</param>
        /// <param name="logger">Logger implementation for this client</param>
        public TestRailAttachmentsClient(Uri testRailServer, string user, string password, TraceListener logger)
            : base(testRailServer, user, password, logger) { }
        #endregion

        #region *** pipeline: get    ***
        /// <summary>
        /// Returns a list of attachments for a test case.
        /// </summary>
        /// <param name="caseId">The ID of the test case</param>
        /// <returns>Success, an array of attachment details is returned in the response</returns>
        public IEnumerable<TestRailAttachment> GetAttachmentsForCase(int caseId)
        {
            // create command
            var command = string.Format(ApiCommands.GET_ATTACHMENTS_FOR_CASE, caseId);

            // execute command
            return ExecuteGet<TestRailAttachment[]>(command);
        }

        /// <summary>
        /// Returns a list of attachments for test results.
        /// </summary>
        /// <param name="testId">The ID of the test</param>
        /// <returns>Success, an array of attachment details is returned in the response</returns>
        public IEnumerable<TestRailAttachment> GetAttachmentsForTest(int testId)
        {
            // create command
            var command = string.Format(ApiCommands.GET_ATTACHMENTS_FOR_TEST, testId);

            // execute command
            return ExecuteGet<TestRailAttachment[]>(command);
        }

        /// <summary>
        /// Returns the requested attachment identified by :attachment_id.
        /// </summary>
        /// <param name="attachmentId">The ID of the attachment</param>
        /// <returns>Success, the attachment is returned in the body of the response</returns>
        public TestRailAttachment GetAttachment(int attachmentId)
        {
            // create command
            var command = string.Format(ApiCommands.GET_ATTACHMENT, attachmentId);

            // execute command
            return ExecuteGet<TestRailAttachment>(command);
        }
        #endregion

        #region *** pipeline: add    ***
        /// <summary>
        /// Adds attachment to a result based on the result ID. The maximum allowable upload size is set to 256mb.
        /// </summary>
        /// <param name="resultId">The ID of the result the attachment should be added to</param>
        /// <param name="fileToUpload">The file to upload</param>
        /// <returns>Success, the attachment ID is returned in the response</returns>
        public TestRailAttachment AddAttachmentToResult(int resultId, string fileToUpload)
        {
            // create command
            var command = string.Format(ApiCommands.ADD_ATTACHMENT_TO_RESULT, resultId);

            // execute command
            return ExecutePost<TestRailAttachment>(command, fileToUpload);
        }

        /// <summary>
        /// Adds attachment to a result based on a combination of result and test case IDs.
        /// </summary>
        /// <param name="resultId">The ID of the result the attachment should be added to</param>
        /// <param name="CaseId">The ID of the test case</param>
        /// <param name="fileToUpload">The file to upload</param>
        /// <returns>Success, the attachment ID is returned in the response</returns>
        public TestRailAttachment AddAttachmentToResultForCase(int resultId, int CaseId, string fileToUpload)
        {
            // create command
            var command = string.Format(ApiCommands.ADD_ATTACHMENT_TO_RESULT_FOR_CASE, resultId, CaseId);

            // execute command
            return ExecutePost<TestRailAttachment>(command, fileToUpload);
        }
        #endregion

        #region *** pipeline: delete ***
        /// <summary>
        /// Deletes the specified attachment identified by :attachment_id.
        /// </summary>
        /// <param name="attachmentId">The ID of the attachment</param>
        public void DeleteAttachment(int attachmentId)
        {
            // create command
            var command = string.Format(ApiCommands.DELETE_ATTACHMENT, attachmentId);

            // execute command
            ExecutePost(command);
        }
        #endregion
    }
}