/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 */
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Automation.TestRail.Extensions
{
    internal static class Utilities
    {
        // constants
        private const StringComparison C = StringComparison.OrdinalIgnoreCase;

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

        /// <summary>
        /// Deserialize a response body into a valid TestRail contract
        /// </summary>
        /// <param name="responseBody">Contract response (as JSON)</param>
        /// <returns>Constructed contract</returns>
        public static IDictionary<string, object> AsCustomFields(this JToken responseBody)
        {
            // load into JToken collection
            var tokens = Write(responseBody);

            // load into custom fields dictionary
            var customFields = new Dictionary<string, object>();
            foreach (var token in tokens)
            {
                customFields.AddOrReplace(((JProperty)token).Name, token.First);
            }
            return customFields;
        }

        private static IEnumerable<JToken> Write(JToken token)
        {
            foreach (var item in token)
            {
                // add custom properties into results
                if (((JProperty)item).Name.StartsWith("custom_", C))
                {
                    yield return item;
                    continue;
                }

                // check for children
                var hasChildren = item.Children().Any();
                if (!hasChildren)
                {
                    continue;
                }
                WriteChildren(item);
            }
        }

        private static void WriteChildren(JToken token)
        {
            foreach (var item in token.Children())
            {
                var isObject = item.Type.ToString().Equals("object", C);
                var isArray = item.Type.ToString().Equals("array", C);
                if (isArray)
                {
                    foreach (var arrayItem in token)
                    {
                        Write(arrayItem);
                    }
                }
                if (!isObject)
                {
                    continue;
                }
                Write(item);
            }
        }
    }
}