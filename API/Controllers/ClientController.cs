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

    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository _clients;
        private readonly IMapper _mapper;
        public ClientsController(IClientRepository clientRepository, IMapper mapper)
        {
            _clients = clientRepository;
            _mapper = mapper;
        }

        private async Task<bool> ClientExists(int id)
        {
            return await _clients.Exist(id);
        }

        [HttpGet]
        [Produces(typeof(DbSet<Client>))]
        public IActionResult GetClient()
        {
            var clients = _clients.GetAll();
            var clientDto = new List<ClientDto>();

            foreach (var client in clients)
            {
                clientDto.Add(_mapper.Map<ClientDto>(client));
            }
            return Ok(clientDto);
        }

        [HttpGet("{id}")]
        [Produces(typeof(Client))]
        public async Task<IActionResult> GetClientById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var client = await _clients.Find(id);

            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        [HttpPut("{id:int}", Name = "UpdateClient")]
        [Produces(typeof(Client))]
        public async Task<IActionResult> UpdateClient([FromRoute] int id, [FromBody] ClientDto clientdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != clientdto.Id)
            {
                return BadRequest();
            }

            var clientObj = _mapper.Map<Client>(clientdto);

            try
            {
                await _clients.Update(clientObj);
                return Ok(clientObj);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ClientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        [HttpPost]
        [Produces(typeof(Client))]
        public async Task<IActionResult> PostClient([FromBody] CreateClientDto clientdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clientObj = _mapper.Map<Client>(clientdto);
            await _clients.Add(clientObj);

            return CreatedAtAction("GetClient", new { id = clientObj.Id }, clientObj);
        }

        [HttpDelete("{id}")]
        [Produces(typeof(Client))]
        public async Task<IActionResult> DeleteClient([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await ClientExists(id))
            {
                return NotFound();
            }

            await _clients.Remove(id);

            return Ok();
        }
    }
}