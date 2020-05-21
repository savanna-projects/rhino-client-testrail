/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 * http://docs.gurock.com/testrail-api2/reference-cases
 */
using Rhino.Client.TestRail.Internal;
using System.Runtime.Serialization;

namespace Rhino.Client.TestRail.Contracts
{
    /// <summary>
    /// Aggregate available searching arguments for get_cases
    /// </summary>
    [DataContract]
    public class GetCasesArguments
    {
        /// <summary>
        /// The ID of the project
        /// </summary>
        [DataMember]
        public int ProjectId { get; set; }

        /// <summary>
        /// The ID of the test suite (optional if the project is operating in single suite mode)
        /// </summary>
        [DataMember]
        public int SuiteId { get; set; }

        /// <summary>
        /// The ID of the section (optional)
        /// </summary>
        [DataMember]
        public int SectionId { get; set; }

        /// <summary>
        /// The number of test cases the response should return
        /// </summary>
        [DataMember]
        public int Limit { get; set; }

        /// <summary>
        /// Where to start counting the tests cases from (the offset)
        /// </summary>
        [DataMember]
        public int Offset { get; set; }

        /// <summary>
        /// Only return cases with matching filter string in the case title
        /// </summary>
        [DataMember]
        public string Filter { get; set; } = string.Empty;

        /// <summary>
        /// Gets a routing based on this module state
        /// </summary>
        /// <returns>Get Cases routing</returns>
        public string RoutingFactory()
        {
            // exit conditions
            if (ProjectId == 0 || SuiteId == 0)
            {
                return string.Empty;
            }

            // generate base route
            var baseRoute = string.Format(ApiCommands.GET_CASES_PROJECT_SUITE, ProjectId, SuiteId);

            // factor based on this module state
            if (SectionId != 0)
            {
                baseRoute += $"&section_id={SectionId}";
            }
            if (Limit != 0)
            {
                baseRoute += $"&limit={Limit}";
            }
            if (Offset != 0)
            {
                baseRoute += $"&offset={Offset}";
            }
            if (!string.IsNullOrEmpty(Filter))
            {
                baseRoute += $"&filter={Filter}";
            }

            // return completed message
            return baseRoute;
        }
    }
}
