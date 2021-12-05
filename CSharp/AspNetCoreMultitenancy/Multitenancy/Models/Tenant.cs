// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace AspNetCoreMultitenancy.Multitenancy
{
    /// <summary>
    /// A Tenant.
    /// </summary>
    public class Tenant
    {
        /// <summary>
        /// Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Description.
        /// </summary>
        public string? Description { get; set; }
    }
}
