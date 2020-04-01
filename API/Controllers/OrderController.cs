using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.DataTransferObjects;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/Orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orders;
        private readonly IMapper _mapper;
        public OrdersController(IOrderRepository ordersRepository, IMapper mapper)
        {
          _orders = ordersRepository;
          _mapper = mapper;
        }
        private async Task<bool> OrderExists(int id)
        {
          return await _orders.Exists(id);
        }

        [HttpGet]
        [Produces(typeof(DbSet<Order>))]
         public IActionResult GetOrder()
        {
            var orders = _orders.GetAll();
            var orderDto = new List<OrderDto>();

            foreach (var order in orders)
            {
                orderDto.Add(_mapper.Map<OrderDto>(order));
            }
            return Ok(orderDto);
        }

        [HttpGet("{id:int}")]
        [Produces(typeof(Order))]
        public async Task<IActionResult> GetOrderById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var order = await _orders.Find(id);

            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPut("{Id:int}", Name = "UpdateOrder")]
        [Produces(typeof(Order))]
        public async Task<IActionResult> UpdateOrder([FromRoute] int id, [FromBody] OrderDto orderdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderdto.Id)
            {
                return BadRequest();
            }   
           var orderObj = _mapper.Map<Order>(orderdto);
            try
            {
                await _orders.Update(orderObj);
                return Ok(orderObj);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        [HttpPost]
        [Produces(typeof(Order))]
        public async Task<IActionResult> PostOrder([FromBody] CreateOrderDto orderdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var orderObj = _mapper.Map<Order>(orderdto);
            try
            {
                await _orders.Add(orderObj);
            }
            catch (DbUpdateException)
            {
                if (!await OrderExists(orderObj.Id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                }
            }
            return CreatedAtAction("GetOrder", new { id = orderObj.Id }, orderObj);
        }

        [HttpDelete("{id}")]
        [Produces(typeof(Order))]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await OrderExists(id))
            {
                return NotFound();
            }

            await _orders.Remove(id);

            return Ok();
        }
    }
}