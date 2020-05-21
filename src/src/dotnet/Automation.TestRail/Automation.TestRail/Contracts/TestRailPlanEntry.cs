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
    [DataContract]
    public class TestRailPlanEntry : Contract
    {
        /// <summary>
        /// The ID of this entry
        /// </summary>
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// The ID of the test suite for the test run(s) (required)
        /// </summary>
        [DataMember]
        public int SuiteId { get; set; }

        /// <summary>
        /// The name of the test run(s)
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// The description of the test run(s) (requires TestRail 5.2 or later)
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// The ID of the user the test run(s) should be assigned to
        /// </summary>
        [DataMember, JsonProperty(PropertyName = "assignedto_id")]
        public int? AssignedTo { get; set; }

        /// <summary>
        /// True for including all test cases of the test suite and false for a custom case selection (default: false)
        /// </summary>
        [DataMember]
        public bool IncludeAll { get; set; }

        /// <summary>
        /// An array of configuration IDs used for the test runs of the test plan entry (requires TestRail 3.1 or later)
        /// </summary>
        [DataMember]
        public int[] ConfigIds { get; set; } = new int[0];

        /// <summary>
        /// An array of case IDs for the custom case selection
        /// </summary>
        [DataMember]
        public int[] CaseIds { get; set; }

        /// <summary>
        /// An array of test runs with configurations, please see the example below for details (requires TestRail 3.1 or later)
        /// </summary>
        [DataMember]
        public TestRailRun[] Runs { get; set; }
    }
}