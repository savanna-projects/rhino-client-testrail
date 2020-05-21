/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 * http://docs.gurock.com/testrail-api2/reference-configs
 */
using System.Runtime.Serialization;

namespace Automation.TestRail.Contracts
{
    [DataContract]
    public class TestRailConfigurationGroup : Contract
    {
        [DataMember]
        public int GroupId { get; set; }

        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// The name of the configuration group (required)
        /// </summary>
        [DataMember]
        public string Name { get; set; }
    }
}