using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AcmeCorpAPI.Models;
using AcmeCorpAPI.Domain;

namespace AcmeCorpAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly AcmeCorpAPIContext _context;

        public CustomersController(AcmeCorpAPIContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomer()
        {
            IEnumerable<Customer> customerList = await _context.Customer.ToListAsync();

            //map domain entities to CustomerDTO
            IEnumerable<CustomerDTO> customerDTOList = from customer in customerList 
                                                        select new CustomerDTO() {
                                                                Id = customer.Id,
                                                                Name = customer.Name, 
                                                                ContactInfo= new(){Email=customer.ContactInfo.Email,
                                                                PhoneNumber=customer.ContactInfo.PhoneNumber}
                                                        };

            return Ok(customerDTOList);

        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customer.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            //return customer;
            return Ok(new CustomerDTO() {
                                Id = customer.Id,
                                Name = customer.Name, 
                                ContactInfo= new(){Email=customer.ContactInfo.Email,
                                PhoneNumber=customer.ContactInfo.PhoneNumber}
                               });
        }

         // GET: api/Customers/5/orders
        [HttpGet("{id}/orders")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetCustomerOrders(int id)
        {
            // navigate the Orders relationship and retrieve the orders for the given customer
            var customer = await _context.Customer.Include(c => c.Orders).FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            IEnumerable<OrderDTO> OrderDTOList = from order in customer.Orders 
                                                        select new OrderDTO() {
                                                            CustomerId = order.CustomerId,
                                                            Details = order.Details,
                                                            Id = order.Id,
                                                            OrderDate = order.OrderDate
                                                        };
            
            return Ok(OrderDTOList);
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, CustomerDTO customerDTO)
        {
            if (id != customerDTO.Id)
            {
                return BadRequest();
            }

            Customer customer = new () { Id = customerDTO.Id,
                                         Name = customerDTO.Name, 
                                         ContactInfo= new(){Email=customerDTO.ContactInfo.Email,
                                                            PhoneNumber=customerDTO.ContactInfo.PhoneNumber} };

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> PostCustomer(CustomerDTO customerDTO)
        {
            Customer customer = new () { Name = customerDTO.Name, 
                                         ContactInfo= new(){Email=customerDTO.ContactInfo.Email,
                                                            PhoneNumber=customerDTO.ContactInfo.PhoneNumber} };

            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();

            customerDTO.Id = customer.Id;

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customerDTO);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }
    }
}
