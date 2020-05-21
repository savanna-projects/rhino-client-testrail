/*
 * CHANGE LOG - keep only last 5 threads
 * 
 */
using Rhino.Client.TestRail.Internal;
using System.Collections.Generic;

namespace Rhino.Client.TestRail.Contracts
{
    /// <summary>
    /// base class for all test-rail contracts
    /// </summary>
    public abstract class Contract : IContext
    {
        /// <summary>
        /// context which can hold extra information about this contract
        /// </summary>
        public IDictionary<string, object> Context { get; } = new Dictionary<string, object>();
    }
}