using TestMSA.library.domain;
using TestMSA.library.domain;
using TestMSA.library.domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TestMSA.library.business;
using NHibernate.Criterion;

namespace TestMSA.library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private ClienteBusiness objClienteBusiness;
        private IMapper _mapper;

        public ClientesController(IMapper mapper)
        {
            this._mapper = mapper;
            this.objClienteBusiness = new ClienteBusiness();
        }

       
        // GET: api/<ClientesController>
        [HttpGet]
        public IActionResult Get()
        {
            List<ClienteDTO> retCliente = _mapper.Map<List<Cliente>, List<ClienteDTO>>(this.objClienteBusiness.GetAll().OrderBy(h => h.Nome).ToList<Cliente>());
            return Ok(retCliente);
        }

        // GET api/<ClientesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            ClienteDTO retCliente = _mapper.Map<Cliente, ClienteDTO>(this.objClienteBusiness.GetById(id));
            return Ok(retCliente);
        }

        // POST api/<ClientesController>
        [HttpPost]
        public IActionResult Post([FromBody] ClienteDTO value)
        {
            Cliente insertCliente = _mapper.Map<ClienteDTO, Cliente>(value);

            if (this.objClienteBusiness.Save(insertCliente))
                return Ok(_mapper.Map<Cliente, ClienteDTO>(insertCliente));
            else
                return BadRequest();
        }

        // PUT api/<ClientesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] ClienteDTO value)
        {
            Cliente updateCliente = this.objClienteBusiness.GetById(id);

            if (updateCliente == null)
                return NotFound();
            _mapper.Map<ClienteDTO, Cliente>(value, updateCliente);
            this.objClienteBusiness.Update(updateCliente);
            return Ok(_mapper.Map<Cliente, ClienteDTO>(updateCliente));
        }

        // DELETE api/<ClientesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            this.objClienteBusiness.Delete(this.objClienteBusiness.GetById(id));
            return NoContent();
        }
    }
}
