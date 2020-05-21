/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 * http://docs.gurock.com/testrail-api2/reference-cases-fields
 */
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Rhino.Client.TestRail.Contracts
{
    /// <summary>
    /// Contract which describes test-rail case field entity
    /// </summary>
    [DataContract]
    public class TestRailCaseField : Contract
    {
        // members: state
        private readonly IList<TestRailCaseFieldConfig> configs;

        // constructors
        public TestRailCaseField()
        {
            configs = new List<TestRailCaseFieldConfig>();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public bool TypeId { get; set; }

        /// <summary>
        /// The type identifier for the new custom field 
        /// </summary>
        /// <see cref="http://docs.gurock.com/testrail-api2/reference-cases-fields"/>
        [DataMember]
        public string Type { get; set; }

        /// <summary>
        /// The name for new the custom field (required)
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string SystemName { get; set; }

        /// <summary>
        /// The label for the new custom field (required)
        /// </summary>
        [DataMember]
        public string Label { get; set; }

        /// <summary>
        /// The description for the new custom field
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// An object wrapped in an array with two default keys, 'context' and 'options'
        /// </summary>
        [DataMember]
        public object Configs { get; set; }

        [DataMember]
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Set flag to true if you want the new custom field included for all templates.
        /// Otherwise (false) specify the ID's of templates to be included as the next parameter (template_ids)
        /// </summary>
        [DataMember]
        public bool IncludeAll { get; set; }

        /// <summary>
        /// ID's of templates new custom field will apply to if include_all is set to false
        /// </summary>
        [DataMember]
        public int[] TemplateIds { get; set; }

        /// <summary>
        /// Add a configuration to this test rail case field entity (fluent)
        /// </summary>
        /// <param name="config">Configuration to add</param>
        /// <returns>Updated instance of this contract</returns>
        public TestRailCaseField AddConfig(TestRailCaseFieldConfig config)
        {
            configs.Add(config);
            return this;
        }

        /// <summary>
        /// Apply all configurations to Configs property
        /// </summary>
        /// <returns>Updates instance of this contract</returns>
        public TestRailCaseField Build()
        {
            Configs = configs;
            return this;
        }
    }
}