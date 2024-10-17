using AutoMapper;
using gebeya01.Dto;
using gebeya01.Models;
using gebeya01.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gebeya01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly PersonRepository _personRepository;
        private readonly IMapper _mapper;

        public OrdersController(ApplicationDbContext context, PersonRepository personRepository, IMapper mapper)
        {
            _context = context;
            _personRepository = personRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdersDto>>> GetOrdersAsync()

        {
            var orders = await _context.Orders.ToListAsync();
            var orderDto = _mapper.Map<List<OrdersDto>>(orders);
            if (orders.Any())
            {
                return NotFound();
            }
            return orderDto;


        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<OrdersDto>>> GetOrderOfUserAsync(int userID)

        {
            var person = _personRepository.GetPersonAsync(userID);
            if(person == null)
            {
                return NotFound("person not found");
            }
            var personsOrders = await _context.Orders.Where(m => m.UserID == userID).ToListAsync();
            var personOrdersDo = _mapper.Map<List<OrdersDto>>(personsOrders);
            return personOrdersDo;

        }
        [HttpPost]
        public async Task<ActionResult> PostOrder([FromBody]OrdersDto orderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var Orders = new Order{
                UserID = orderDto.UserID,
                OrderDate = orderDto.OrderDate,
                TotalAmount = orderDto.TotalAmount,
                Status = orderDto.Status,
                ShippingAddress = orderDto.ShippingAddress,

            };


            _context.Orders.Add(Orders);
            _context.Update(Orders);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetOrdersAsync", new { OrderID = Orders.OrderID }, orderDto);
        }


   }

}
