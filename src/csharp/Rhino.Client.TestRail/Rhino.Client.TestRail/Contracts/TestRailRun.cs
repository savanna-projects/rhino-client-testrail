/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 * http://docs.gurock.com/testrail-api2/reference-runs
 */
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Rhino.Client.TestRail.Contracts
{
    /// <summary>
    /// contract which describes test-rail test-run entity
    /// </summary
    [DataContract]
    public class TestRailRun : Contract
    {
        /// <summary>
        /// gets or sets the ID of the user the entire test run is assigned to
        /// </summary>
        [DataMember, JsonProperty(PropertyName = "assignedto_id")]
        public int? AssignedTo { get; set; }

        /// <summary>
        /// gets or sets the amount of tests in the test run marked as blocked
        /// </summary>
        [DataMember]
        public int BlockedCount { get; set; }

        /// <summary>
        /// gets or sets the date/time when the test run was closed (as UNIX time-stamp)
        /// </summary>
        [DataMember]
        public int? CompletedOn { get; set; }

        /// <summary>
        /// gets or sets the configuration of the test run as string (if part of a test plan)
        /// </summary>
        [DataMember]
        public string Config { get; set; }

        /// <summary>
        /// gets or sets the array of IDs of the configurations of the test run (if part of a test plan)
        /// </summary>
        [DataMember]
        public int[] ConfigIds { get; set; }

        /// <summary>
        /// An array of case IDs for the custom case selection
        /// </summary>
        [DataMember]
        public int[] CaseIds { get; set; }

        /// <summary>
        /// gets or sets the ID of the user who created the test run
        /// </summary>
        [DataMember]
        public int? CreatedBy { get; set; }

        /// <summary>
        /// gets or sets the date/time when the test run was created (as UNIX time-stamp)
        /// </summary>
        [DataMember]
        public int? CreatedOn { get; set; }

        /// <summary>
        /// gets or sets the amount of tests in the test run with the respective custom status
        /// </summary>
        [DataMember]
        public int CustomStatus1Count { get; set; }

        /// <summary>
        /// gets or sets the amount of tests in the test run with the respective custom status
        /// </summary>
        [DataMember]
        public int CustomStatus2Count { get; set; }

        /// <summary>
        /// gets or sets the amount of tests in the test run with the respective custom status
        /// </summary>
        [DataMember]
        public int CustomStatus3Count { get; set; }

        /// <summary>
        /// gets or sets the amount of tests in the test run with the respective custom status
        /// </summary>
        [DataMember]
        public int CustomStatus4Count { get; set; }

        /// <summary>
        /// gets or sets the amount of tests in the test run with the respective custom status
        /// </summary>
        [DataMember]
        public int CustomStatus5Count { get; set; }

        /// <summary>
        /// gets or sets the amount of tests in the test run with the respective custom status
        /// </summary>
        [DataMember]
        public int CustomStatus6Count { get; set; }

        /// <summary>
        /// gets or sets the amount of tests in the test run with the respective custom status
        /// </summary>
        [DataMember]
        public int CustomStatus7Count { get; set; }

        /// <summary>
        /// gets or sets the description of the test run
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string EntryId { get; set; }

        [DataMember]
        public int EntryIndex { get; set; }

        /// <summary>
        /// gets or sets the amount of tests in the test run marked as failed
        /// </summary>
        [DataMember]
        public int FailedCount { get; set; }

        /// <summary>
        /// gets or sets the unique ID of the test run
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// gets or set a value to true if the test run includes all test cases and false otherwise
        /// </summary>
        [DataMember]
        public bool IncludeAll { get; set; }

        /// <summary>
        /// gets or set a value to true if the test run was closed and false otherwise
        /// </summary>
        [DataMember]
        public bool? IsCompleted { get; set; }

        /// <summary>
        /// gets or sets the ID of the milestone this test run belongs to
        /// </summary>
        [DataMember]
        public int? MilestoneId { get; set; }

        /// <summary>
        /// gets or sets the ID of the test plan this test run belongs to
        /// </summary>
        [DataMember]
        public int PlanId { get; set; }

        /// <summary>
        /// gets or sets the name of the test run
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// gets or sets the amount of tests in the test run marked as passed
        /// </summary>
        [DataMember]
        public int PassedCount { get; set; }

        /// <summary>
        /// gets or sets the ID of the project this test run belongs to
        /// </summary>
        [DataMember]
        public int ProjectId { get; set; }

        /// <summary>
        /// gets or sets the amount of tests in the test run marked as retest
        /// </summary>
        [DataMember]
        public int RetestCount { get; set; }

        /// <summary>
        /// gets or sets the ID of the test suite this test run is derived from
        /// </summary>
        [DataMember]
        public int SuiteId { get; set; }

        /// <summary>
        /// gets or sets the amount of tests in the test run marked as untested
        /// </summary>
        [DataMember]
        public int UntestedCount { get; set; }

        /// <summary>
        /// gets or sets the address/URL of the test run in the user interface
        /// </summary>
        [DataMember]
        public string Url { get; set; }
    }
}