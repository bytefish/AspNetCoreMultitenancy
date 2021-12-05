// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;

namespace AspNetCoreMultitenancy.Dto
{
    /// <summary>
    /// A Customer.
    /// </summary>
    public class CustomerDto
    {
        /// <summary>
        /// Id.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// First Name.
        /// </summary>
        [JsonPropertyName("firstName")]
        public string? FirstName { get; set; }

        /// <summary>
        /// Last Name.
        /// </summary>
        [JsonPropertyName("lastName")]
        public string? LastName { get; set; }
    }
}