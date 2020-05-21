/*
 * CHANGE LOG - keep only last 5 threads
 */
namespace Automation.TestRail.Contracts
{
    public class TestRailHttpCommand
    {
        /// <summary>
        /// HTTP method to use with this command
        /// </summary>
        public string HttpMethod { get; set; } = "GET";

        /// <summary>
        /// HTTP command endpoint, including routing, not including server base URL
        /// </summary>
        public string Endpoint { get; set; }

        /// <summary>
        /// gets or sets this command content type
        /// </summary>
        public string ContentType { get; set; } = "application/json";

        /// <summary>
        /// data object to send with this command (request body)
        /// </summary>
        public object Data { get; set; }
    }
}