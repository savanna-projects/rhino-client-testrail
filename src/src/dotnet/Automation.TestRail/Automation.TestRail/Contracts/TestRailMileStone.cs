/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 * http://docs.gurock.com/testrail-api2/reference-milestones
 */
using System.Runtime.Serialization;

namespace Automation.TestRail.Contracts
{
    /// <summary>
    /// contract which describes test-rail test-run entity
    /// </summary>
    [DataContract]
    public class TestRailMileStone : Contract
    {
        /// <summary>
        /// The date/time when the milestone was marked as completed (as UNIX times-tamp)
        /// </summary>
        [DataMember]
        public int? CompletedOn { get; set; }

        /// <summary>
        /// The description of the milestone
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// The due date/time of the milestone (as UNIX time-stamp)
        /// </summary>
        [DataMember]
        public int? DueOn { get; set; }

        /// <summary>
        /// The unique ID of the milestone
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// True if the milestone is marked as completed and false otherwise
        /// </summary>
        [DataMember]
        public bool IsCompleted { get; set; }

        /// <summary>
        /// True if the milestone is marked as started and false otherwise (available since TestRail 5.3)
        /// </summary>
        [DataMember]
        public bool IsStarted { get; set; }

        /// <summary>
        /// The sub milestones that belong to the milestone (if any);
        /// only available with get_milestone (available since TestRail 5.3)
        /// </summary>
        [DataMember]
        public TestRailMileStone[] Milestones { get; set; }

        /// <summary>
        /// The name of the milestone
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// The ID of the parent milestone the milestone belongs to (if any) (available since TestRail 5.3)
        /// </summary>
        [DataMember]
        public int? ParentId { get; set; }

        /// <summary>
        /// The ID of the project the milestone belongs to
        /// </summary>
        [DataMember]
        public int ProjectId { get; set; }

        /// <summary>
        /// The scheduled start date/time of the milestone (as UNIX time-stamp) (available since TestRail 5.3)
        /// </summary>
        [DataMember]
        public int? StartOn { get; set; }

        /// <summary>
        /// The date/time when the milestone was started (as UNIX time-stamp) (available since TestRail 5.3)
        /// </summary>
        [DataMember]
        public int? StartedOn { get; set; }

        /// <summary>
        /// The address/URL of the milestone in the user interface
        /// </summary>
        [DataMember]
        public string Url { get; set; }
    }
}