using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Consultorio.DataAccess;
using Consultorio.Models;
using Consultorio.Request;
using Consultorio.Response;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Consultorio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public PacienteController(ApplicationDbContext dbContext, IMapper mapper )
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<List<PacienteResponse>> GetPacientes()
        {
            var pacientes = await _dbContext.Pacientes.Include(x => x.Citas).ToListAsync().ConfigureAwait(false);
            var pacientesResponse = _mapper.Map<List<PacienteResponse>>(pacientes);

            return pacientesResponse;

        }

        [HttpGet("{id}", Name = "getPaciente")]
        public async Task<ActionResult<PacienteResponse>> GetPaciente(int id)
        {
            var paciente = await _dbContext.Pacientes.Include(x => x.Citas)
                                .FirstOrDefaultAsync(x => x.PacienteId == id).ConfigureAwait(false);
            
            if (paciente == null) { return BadRequest(); }

            var pacienteResponse = _mapper.Map<PacienteResponse>(paciente);

            return pacienteResponse;

        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> PostPaciente([FromBody] PacienteRequest pacienteRequest)
        {
            var paciente = _mapper.Map<Paciente>(pacienteRequest);
            var newPaciente = await _dbContext.Pacientes.AddAsync(paciente);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            var pacienteResponse = _mapper.Map<PacienteResponse>(paciente);

            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> PutPaciente(int id, [FromBody] PacienteRequest pacienteRequest)
        {
            var paciente = _mapper.Map<Paciente>(pacienteRequest);
            paciente.PacienteId = id;


            _dbContext.Entry(paciente).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return NoContent();

        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> DeletePaciente(int id)
        {
            var paciente = await _dbContext.Pacientes
                                 .FirstOrDefaultAsync(x => x.PacienteId == id).ConfigureAwait(false);
            
            if (paciente == null) { return BadRequest(); }

            _dbContext.Pacientes.Remove(paciente);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            var pacienteResponse = _mapper.Map<PacienteResponse>(paciente);

            return Ok(pacienteResponse);
        }
    }
}
