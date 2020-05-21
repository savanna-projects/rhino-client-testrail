/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 * http://docs.gurock.com/testrail-api2/reference-results
 */
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Rhino.Client.TestRail.Contracts
{
    [DataContract]
    public class TestRailResult
    {
        /// <summary>
        /// The ID of the assignee (user) of the test result
        /// </summary>
        [DataMember, JsonProperty(PropertyName = "assignedto_id")]
        public int? AssignedTo { get; set; }

        /// <summary>
        /// The comment or error message of the test result
        /// </summary>
        [DataMember]
        public string Comment { get; set; }

        /// <summary>
        /// The ID of the user who created the test result
        /// </summary>
        [DataMember]
        public int CreatedBy { get; set; }

        /// <summary>
        /// The date/time when the test result was created (as UNIX time-stamp)
        /// </summary>
        [DataMember]
        public int CreatedOn { get; set; }

        [DataMember]
        public int CustomSeverity { get; set; }

        [DataMember]
        public CustomStep[] CustomStepResults { get; set; }

        [DataMember]
        public int CustomTolerance { get; set; }

        /// <summary>
        /// A comma-separated list of defects linked to the test result
        /// </summary>
        [DataMember]
        public string Defects { get; set; }

        /// <summary>
        /// The amount of time it took to execute the test (e.g. "1m" or "2m 30s")
        /// </summary>
        [DataMember]
        public string Elapsed { get; set; }

        /// <summary>
        /// The unique ID of the test result
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// The status of the test result, e.g. passed or failed, also see get_statuses
        /// </summary>
        [DataMember]
        public int StatusId { get; set; }

        /// <summary>
        /// The ID of the test this test result belongs to
        /// </summary>
        [DataMember]
        public int TestId { get; set; }

        /// <summary>
        /// The version or build you tested against
        /// </summary>
        [DataMember]
        public string Version { get; set; }
    }
}