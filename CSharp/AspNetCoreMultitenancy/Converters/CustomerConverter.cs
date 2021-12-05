// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using AspNetCoreMultitenancy.Dto;
using AspNetCoreMultitenancy.Models;

namespace AspNetCoreMultitenancy.Converters
{
    public static class CustomerConverter
    {
        public static Customer ToModel(CustomerDto source)
        {
            return new Customer
            {
                Id = source.Id,
                FirstName = source.FirstName,
                LastName = source.LastName,
            };
        }

        public static CustomerDto ToDto(Customer source)
        {
            return new CustomerDto
            {
                Id = source.Id,
                FirstName = source.FirstName,
                LastName = source.LastName,
            };
        }
    }
}