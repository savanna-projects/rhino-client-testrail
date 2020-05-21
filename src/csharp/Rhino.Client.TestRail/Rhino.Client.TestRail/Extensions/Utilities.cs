/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 */
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Diagnostics;
using System.IO;

namespace Rhino.Client.TestRail.Extensions
{
    internal static class Utilities
    {
        /// <summary>
        /// Gets a trace text-writer trace listener
        /// </summary>
        /// <param name="applicationName">Application under logging (will be applied to the file and listener names)</param>
        /// <param name="directory">Target folder to write the logs into</param>
        /// <returns>A new instance of the System.Diagnostics.TextWriterTraceListener</returns>
        public static TraceListener GetTraceListener(string applicationName, string directory)
        {
            // shortcuts
            var logName = $"{applicationName}-{DateTime.Now.ToString("yyyyMMdd")}.log";

            // settings
            Trace.AutoFlush = true;

            // initialize listener
            Directory.CreateDirectory($"{directory}");
            return new TextWriterTraceListener($"{directory}\\{logName}", applicationName);
        }

        /// <summary>
        /// Gets Json Setting based on naming strategy
        /// </summary>
        /// <typeparam name="T">Naming strategy type</typeparam>
        /// <returns>The settings applied on a Newtonsoft.Json.JsonSerializer object</returns>
        public static JsonSerializerSettings GetJsonSettings<T>() where T : NamingStrategy, new()
        {
            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new T()
            };
            return new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            };
        }
    }
}