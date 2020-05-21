/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 * http://docs.gurock.com/testrail-api2/reference-configs
 */
using System.Runtime.Serialization;

namespace Automation.TestRail.Contracts
{
    /// <summary>
    /// Contract which describes test-rail configuration entity
    /// </summary>
    [DataContract]
    public class TestRailConfiguration : Contract
    {
        [DataMember]
        public TestRailConfigurationGroup[] Configs { get; set; }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int ProjectId { get; set; }

        [DataMember]
        public int GroupId { get; set; }
    }
}