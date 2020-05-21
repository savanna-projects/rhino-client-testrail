/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 * http://docs.gurock.com/testrail-api2/reference-cases
 */
using System.Runtime.Serialization;

namespace Rhino.Client.TestRail.Contracts
{
    /// <summary>
    /// contract which describes test-rail test-case entity
    /// </summary>
    [DataContract]
    public class TestRailCase : Contract
    {
        /// <summary>
        /// gets or sets the ID of the user who created the test case
        /// </summary>
        [DataMember]
        public int? CreatedBy { get; set; }

        /// <summary>
        /// gets or sets the date/time when the test case was created (as UNIX time-stamp)
        /// </summary>
        [DataMember]
        public int? CreatedOn { get; set; }

        [DataMember]
        public int? DisplayOrder { get; set; }

        /// <summary>
        /// gets or sets the estimate, e.g. "30s" or "1m 45s"
        /// </summary>
        [DataMember]
        public string Estimate { get; set; }

        /// <summary>
        /// gets or sets the estimate forecast, e.g. "30s" or "1m 45s"
        /// </summary>
        [DataMember]
        public string EstimateForecast { get; set; }

        /// <summary>
        /// gets or sets the unique ID of the test case
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// gets or sets the ID of the milestone that is linked to the test case
        /// </summary>
        [DataMember]
        public int? MilestoneId { get; set; }

        /// <summary>
        /// gets or sets the ID of the priority that is linked to the test case
        /// </summary>
        [DataMember]
        public int? PriorityId { get; set; }

        /// <summary>
        /// gets or sets a comma-separated list of references/requirements
        /// </summary>
        [DataMember]
        public string Refs { get; set; }

        /// <summary>
        /// gets or sets the ID of the section the test case belongs to
        /// </summary>
        [DataMember]
        public int? SectionId { get; set; }

        /// <summary>
        /// gets or sets the ID of the suite the test case belongs to
        /// </summary>
        [DataMember]
        public int? SuiteId { get; set; }

        /// <summary>
        /// gets or sets the ID of the template (field layout) the test case uses (requires TestRail 5.2 or later)
        /// </summary>
        [DataMember]
        public int? TemplateId { get; set; }

        /// <summary>
        /// gets or sets the title of the test case
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// gets or sets the ID of the test case type that is linked to the test case
        /// </summary>
        [DataMember]
        public int? TypeId { get; set; }

        /// <summary>
        /// gets or sets the ID of the user who last updated the test case
        /// </summary>
        [DataMember]
        public int? UpdatedBy { get; set; }

        /// <summary>
        /// gets or sets the date/time when the test case was last updated (as UNIX time-stamp)
        /// </summary>
        [DataMember]
        public int? UpdatedOn { get; set; }
    }
}