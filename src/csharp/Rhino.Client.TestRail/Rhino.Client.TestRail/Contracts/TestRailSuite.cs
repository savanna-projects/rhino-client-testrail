/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 * http://docs.gurock.com/testrail-api2/reference-suites
 */
using System.Runtime.Serialization;

namespace Rhino.Client.TestRail.Contracts
{
    /// <summary>
    /// contract which describes test-rail test-run entity
    /// </summary>
    [DataContract]
    public class TestRailSuite
    {
        /// <summary>
        /// get or sets the date/time when the test suite was closed (as UNIX time-stamp)
        /// added with TestRail 4.0
        /// </summary>
        [DataMember]
        public int? CompletedOn { get; set; }

        /// <summary>
        /// gets or sets the description of the test suite
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// gets or sets the unique ID of the test suite
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// gets or set a value to true if the test suite is a baseline test suite and false otherwise (added with TestRail 4.0)
        /// </summary>
        [DataMember]
        public bool? IsBaseline { get; set; }

        /// <summary>
        /// gets or set a value to true if the test suite is marked as completed/archived and false otherwise (added with TestRail 4.0)
        /// </summary>
        [DataMember]
        public bool? IsCompleted { get; set; }

        /// <summary>
        /// gets or set a value to true if the test suite is a master test suite and false otherwise (added with TestRail 4.0)
        /// </summary>
        [DataMember]
        public bool? IsMaster { get; set; }

        /// <summary>
        /// gets or sets the name of the test suite
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// gets or sets the ID of the project this test suite belongs to
        /// </summary>
        [DataMember]
        public int? ProjectId { get; set; }

        /// <summary>
        /// gets or sets the address/URL of the test suite in the user interface
        /// </summary>
        [DataMember]
        public string Url { get; set; }
    }
}