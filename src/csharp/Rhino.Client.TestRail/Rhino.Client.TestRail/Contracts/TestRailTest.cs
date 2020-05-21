/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 * http://docs.gurock.com/testrail-api2/reference-tests
 */
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Rhino.Client.TestRail.Contracts
{
    [DataContract]
    public class TestRailTest : TestRailCase
    {
        /// <summary>
        /// The ID of the user the test is assigned to
        /// </summary>
        [DataMember, JsonProperty(PropertyName = "assignedto_id")]
        public int? AssignedTo { get; set; }

        /// <summary>
        /// The ID of the related test case
        /// </summary>
        [DataMember]
        public int CaseId { get; set; }

        /// <summary>
        /// The ID of the test run the test belongs to
        /// </summary>
        [DataMember]
        public int RunId { get; set; }

        /// <summary>
        /// The ID of the current status of the test, also see get_statuses
        /// </summary>
        [DataMember]
        public int StatusId { get; set; }
    }
}