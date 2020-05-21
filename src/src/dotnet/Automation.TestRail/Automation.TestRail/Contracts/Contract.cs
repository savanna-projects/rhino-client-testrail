/*
 * CHANGE LOG - keep only last 5 threads
 * 
 */
using Automation.TestRail.Internal;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Automation.TestRail.Contracts
{
    /// <summary>
    /// base class for all test-rail contracts
    /// </summary>
    [DataContract]
    public abstract class Contract : IContext
    {
        /// <summary>
        /// Context which can hold extra information about this contract.
        /// </summary>
        [DataMember]
        public IDictionary<string, object> Context { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Gets a collection of user-defined fields that are registered with the server.
        /// </summary>
        [DataMember]
        public IDictionary<string, object> CustomFields { get; set; }
    }
}