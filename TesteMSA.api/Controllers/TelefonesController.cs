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

namespace TestMSA.library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelefonesController : ControllerBase
    {
        private TelefoneBusiness objTelefoneBusiness;
        private IMapper _mapper;

        public TelefonesController(IMapper mapper)
        {
            this._mapper = mapper;
            this.objTelefoneBusiness = new TelefoneBusiness();
        }


        // GET: api/<TelefonesController>
        [HttpGet("cliente/{idCliente}")]
        public IActionResult GetByCliente(Guid idCliente)
        {
            List<TelefoneDTO> retTelefone = _mapper.Map<List<Telefone>, List<TelefoneDTO>>(new ClienteBusiness().GetById(idCliente).Telefones.ToList<Telefone>());
            return Ok(retTelefone);
        }

        // GET api/<TelefonesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            TelefoneDTO retTelefone = _mapper.Map<Telefone, TelefoneDTO>(this.objTelefoneBusiness.GetById(id));
            return Ok(retTelefone);
        }

        // POST api/<TelefonesController>
        [HttpPost]
        public IActionResult Post([FromBody] TelefoneDTO value)
        {
            ClienteBusiness srvCliente = new ClienteBusiness();
            Cliente objCliente = srvCliente.GetById(value.ClienteId);


            Telefone insertTelefone = _mapper.Map<TelefoneDTO, Telefone>(value);
            objCliente.Telefones.Add(insertTelefone);
            srvCliente.Update(objCliente);

            return Ok(_mapper.Map<Telefone, TelefoneDTO>(insertTelefone));
        }

        // PUT api/<TelefonesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] TelefoneDTO value)
        {
            Telefone updateTelefone = this.objTelefoneBusiness.GetById(id);

            if (updateTelefone == null)
                return NotFound();
            _mapper.Map<TelefoneDTO, Telefone>(value, updateTelefone);
            this.objTelefoneBusiness.Update(updateTelefone);
            return Ok(_mapper.Map<Telefone, TelefoneDTO>(updateTelefone));
        }

        // DELETE api/<TelefonesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            this.objTelefoneBusiness.Delete(this.objTelefoneBusiness.GetById(id));
            return NoContent();
        }
    }
}
