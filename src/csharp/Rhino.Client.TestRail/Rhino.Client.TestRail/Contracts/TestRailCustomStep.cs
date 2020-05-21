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
    /// contract which describes test-rail test-step entity
    /// </summary>
    [DataContract]
    public class TestRailCustomStep
    {
        [DataMember]
        public string Content { get; set; }

        [DataMember]
        public string Expected { get; set; }

        [DataMember]
        public string Actual { get; set; }

        [DataMember]
        public int? StatusId { get; set; }
    }
}