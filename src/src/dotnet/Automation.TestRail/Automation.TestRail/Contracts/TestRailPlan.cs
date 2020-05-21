/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 * http://docs.gurock.com/testrail-api2/reference-plans
 */
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Automation.TestRail.Contracts
{
    /// <summary>
    /// contract which describes test-rail plan entity
    /// </summary>
    [DataContract]
    public class TestRailPlan : Contract
    {
        /// <summary>
        /// The ID of the user the test run(s) should be assigned to
        /// </summary>
        [DataMember, JsonProperty(PropertyName = "assignedto_id")]
        public int? AssignedTo { get; set; }

        /// <summary>
        /// The amount of tests in the test plan marked as blocked
        /// </summary>
        [DataMember]
        public int BlockedCount { get; set; }

        /// <summary>
        /// The date/time when the test plan was closed (as UNIX time-stamp)
        /// </summary>
        [DataMember]
        public int? CompletedOn { get; set; }

        /// <summary>
        /// The ID of the user who created the test plan
        /// </summary>
        [DataMember]
        public int CreatedBy { get; set; }

        /// <summary>
        /// The date/time when the test plan was created (as UNIX time-stamp)
        /// </summary>
        [DataMember]
        public int CreatedOn { get; set; }

        /// <summary>
        /// The description of the test plan
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// An array of 'entries', i.e. group of test runs
        /// </summary>
        [DataMember]
        public TestRailPlanEntry[] Entries { get; set; }

        /// <summary>
        /// The amount of tests in the test plan marked as failed
        /// </summary>
        [DataMember]
        public int FailedCount { get; set; }

        /// <summary>
        /// The unique ID of the test plan
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// True if the test plan was closed and false otherwise
        /// </summary>
        [DataMember]
        public bool IsCompleted { get; set; }

        /// <summary>
        /// The ID of the milestone this test plan belongs to
        /// </summary>
        [DataMember]
        public int? MilestoneId { get; set; }

        /// <summary>
        /// The name of the test plan
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// The amount of tests in the test plan marked as passed
        /// </summary>
        [DataMember]
        public int PassedCount { get; set; }

        /// <summary>
        /// The ID of the project this test plan belongs to
        /// </summary>
        [DataMember]
        public int ProjectId { get; set; }

        /// <summary>
        /// The amount of tests in the test plan marked as retest
        /// </summary>
        [DataMember]
        public int RetestCount { get; set; }

        /// <summary>
        /// The amount of tests in the test plan marked as untested
        /// </summary>
        [DataMember]
        public int UntestedCount { get; set; }

        /// <summary>
        /// The address/URL of the test plan in the user interface
        /// </summary>
        [DataMember]
        public string Url { get; set; }
    }
}