/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 * http://docs.gurock.com/testrail-api2/reference-cases-fields
 */
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Automation.TestRail.Contracts
{
    [DataContract]
    public class TestRailCaseFieldConfig
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public Dictionary<string, object> Context { get; set; }

        [DataMember]
        public Dictionary<string, object> Options { get; set; }
    }
}
