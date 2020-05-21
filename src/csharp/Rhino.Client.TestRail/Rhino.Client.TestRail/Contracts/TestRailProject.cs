/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 * http://docs.gurock.com/testrail-api2/reference-projects
 */
using System.Runtime.Serialization;

namespace Rhino.Client.TestRail.Contracts
{
    /// <summary>
    /// contract which describes test-rail project entity
    /// </summary>
    [DataContract]
    public class TestRailProject : Contract
    {
        /// <summary>
        /// gets or sets the description/announcement of the project
        /// </summary>
        [DataMember]
        public string Announcement { get; set; }

        /// <summary>
        /// gets or sets the date/time when the project was marked as completed (as UNIX time-stamp)
        /// </summary>
        [DataMember]
        public int? CompletedOn { get; set; }

        /// <summary>
        /// gets or sets the unique ID of the project
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// gets or sets a value to true if the project is marked as completed and false otherwise
        /// </summary>
        [DataMember]
        public bool? IsCompleted { get; set; }

        /// <summary>
        /// gets or sets the name of the project
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// gets or sets a value to true to show the announcement/description and false otherwise
        /// </summary>
        [DataMember]
        public bool? ShowAnnouncement { get; set; }

        /// <summary>
        /// gets or sets the suite mode of the project (1 for single suite mode, 2 for single suite + baselines, 3 for multiple suites)
        /// added with TestRail 4.0
        /// </summary>
        [DataMember]
        public int? SuiteMode { get; set; }

        /// <summary>
        /// gets or sets the address/URL of the project in the user interface
        /// </summary>
        [DataMember]
        public string Url { get; set; }
    }
}