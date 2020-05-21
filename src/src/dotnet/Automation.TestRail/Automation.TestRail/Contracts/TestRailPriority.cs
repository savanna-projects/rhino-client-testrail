/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 * http://docs.gurock.com/testrail-api2/reference-priorities
 */
using System.Runtime.Serialization;

namespace Automation.TestRail.Contracts
{
    /// <summary>
    /// contract which describes test-rail priority entity
    /// </summary>
    [DataContract]
    public class TestRailPriority : Contract
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public bool IsDefault { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Priority { get; set; }

        [DataMember]
        public string ShortName { get; set; }
    }
}