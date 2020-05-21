/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 * http://docs.gurock.com/testrail-api2/reference-sections
 */
using System.Runtime.Serialization;

namespace Automation.TestRail.Contracts
{
    /// <summary>
    /// contract which describes test-rail section entity
    /// </summary>
    [DataContract]
    public class TestRailSection : Contract
    {
        /// <summary>
        /// The level in the section hierarchy of the test suite
        /// </summary>
        [DataMember]
        public int Depth { get; set; }

        /// <summary>
        /// The description of the section (added with TestRail 4.0)
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// The order in the test suite
        /// </summary>
        [DataMember]
        public int DisplayOrder { get; set; }

        /// <summary>
        /// The unique ID of the section
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// The ID of the parent section in the test suite
        /// </summary>
        [DataMember]
        public int? ParentId { get; set; }

        /// <summary>
        /// The name of the section
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// The ID of the test suite this section belongs to
        /// </summary>
        [DataMember]
        public int SuiteId { get; set; }
    }
}