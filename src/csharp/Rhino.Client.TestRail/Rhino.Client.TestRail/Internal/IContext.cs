/*
 * CHANGE LOG - keep only last 5 threads
 * 
 */
using System.Collections.Generic;

namespace Rhino.Client.TestRail.Internal
{
    internal interface IContext
    {
        /// <summary>
        /// context which can hold extra information about this contract
        /// </summary>
        IDictionary<string, object> Context { get; }
    }
}
