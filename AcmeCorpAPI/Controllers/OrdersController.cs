using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AcmeCorpAPI.Models;
using AcmeCorpAPI.Domain;

namespace AcmeCorpAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AcmeCorpAPIContext _context;

        public OrdersController(AcmeCorpAPIContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrder()
        {
            IEnumerable<Order> orderList =  await _context.Order.ToListAsync();

            //map domain entities to CustomerDTO
            IEnumerable<OrderDTO> orderDTOList = from order in orderList 
                                                        select new OrderDTO() {
                                                                Id = order.Id,
                                                                Details = order.Details,
                                                                OrderDate = order.OrderDate,
                                                                CustomerId = order.CustomerId
                                                        };

            return Ok(orderDTOList);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Order.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(new OrderDTO() {
                            Id = order.Id,
                            Details = order.Details,
                            OrderDate = order.OrderDate,
                            CustomerId = order.CustomerId
                      });
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, OrderDTO orderDTO)
        {
            if (id != orderDTO.Id)
            {
                return BadRequest();
            }

            Order order = new Order() {
                                        Id = orderDTO.Id,
                                        Details = orderDTO.Details,
                                        OrderDate = orderDTO.OrderDate,
                                        CustomerId = orderDTO.CustomerId
                                    };

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderDTO>> PostOrder(OrderDTO orderDTO)
        {
            Order order = new () { CustomerId = orderDTO.CustomerId, 
                                   Details = orderDTO.Details,
                                   OrderDate = orderDTO.OrderDate} ;

            try{
            _context.Order.Add(order);
            await _context.SaveChangesAsync();
            } 
            catch (DbUpdateException)
            {
                // foreign key exception - associated customer does not exist
                if (!CustomerExists(orderDTO.CustomerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            orderDTO.Id = order.Id;
            return CreatedAtAction("GetOrder", new { id = order.Id }, orderDTO);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }
    }
}
