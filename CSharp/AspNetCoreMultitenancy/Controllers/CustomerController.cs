// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using AspNetCoreMultitenancy.Converters;
using AspNetCoreMultitenancy.Database;
using AspNetCoreMultitenancy.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreMultitenancy.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDbContext;

        public CustomerController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var customers = await applicationDbContext.Customers
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var customerDtos = customers?
                .Select(customer => CustomerConverter.ToDto(customer))
                .ToList();

            return Ok(customerDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            var customer = await applicationDbContext.Customers.FindAsync(id);

            if(customer == null)
            {
                return NotFound();
            }

            var customerDto = CustomerConverter.ToDto(customer);

            return Ok(customerDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerDto customerDto, CancellationToken cancellationToken)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = CustomerConverter.ToModel(customerDto);

            await applicationDbContext.AddAsync(customer, cancellationToken);
            await applicationDbContext.SaveChangesAsync(cancellationToken);

            return CreatedAtAction(nameof(Get), new { id = customer.Id }, CustomerConverter.ToDto(customer));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CustomerDto customerDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = CustomerConverter.ToModel(customerDto);

            applicationDbContext.Customers.Update(customer);
            await applicationDbContext.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var customer = await applicationDbContext.Customers.FindAsync(id);

            if(customer == null)
            {
                return NotFound();
            }

            applicationDbContext.Customers.Remove(customer);

            await applicationDbContext.SaveChangesAsync(cancellationToken);

            return NoContent();
        }
    }
}
