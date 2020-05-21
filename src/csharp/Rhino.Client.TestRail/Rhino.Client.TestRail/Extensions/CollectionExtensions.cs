/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 */
using System;
using System.Diagnostics;
using System.Linq;

namespace Rhino.Client.TestRail.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Adds a System.Diagnostics.TraceListener to the list if not exists
        /// </summary>
        /// <param name="listenerCollection">Provides a thread-safe list of System.Diagnostics.TraceListener objects.</param>
        /// <param name="listener">Provides the abstract base class for the listeners who monitor trace and debug output.</param>
        /// <param name="applicationName">The name of the application this Trace instance is logging</param>
        public static void AddIfNotExists(this TraceListenerCollection listenerCollection, TraceListener listener, string applicationName)
        {
            // constants
            const StringComparison C = StringComparison.OrdinalIgnoreCase;

            // add if not exists
            var listenerExists = listenerCollection.Cast<TraceListener>().Any(l => l.Name.Equals(applicationName, C));
            if (listenerExists)
            {
                return;
            }
            Trace.Listeners.Add(listener);
        }
    }
}