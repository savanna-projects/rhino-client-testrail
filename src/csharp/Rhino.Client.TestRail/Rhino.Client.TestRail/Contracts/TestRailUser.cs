/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 * http://docs.gurock.com/testrail-api2/reference-users
 */
using System.Runtime.Serialization;

namespace Rhino.Client.TestRail.Contracts
{
    /// <summary>
    /// contract which describes test-rail user entity
    /// </summary>
    [DataContract]
    public class TestRailUser
    {
        /// <summary>
        /// The email address of the user as configured in TestRail
        /// </summary>
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        /// The unique ID of the user
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// True if the user is active and false otherwise
        /// </summary>
        [DataMember]
        public bool IsActive { get; set; }

        /// <summary>
        /// The full name of the user
        /// </summary>
        [DataMember]
        public string Name { get; set; }
    }
}