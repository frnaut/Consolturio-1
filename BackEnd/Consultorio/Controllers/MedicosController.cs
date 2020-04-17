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

namespace Consultorio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicosController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public MedicosController(ApplicationDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<MedicoResponse>>> GetMedicos()
        {
            var medicos = await _dbContext.Medicos.Include(x => x.Especialidad)
                                                  .Include(x => x.Citas).ToListAsync().ConfigureAwait(false);

            var medicosResponse = _mapper.Map<List<MedicoResponse>>(medicos);

            return medicosResponse;
        }

        [HttpGet("{id}", Name = "getMedico")]
        public async Task<ActionResult<MedicoResponse>> GetMedico(int id)
        {
            var medico = await _dbContext.Medicos.Include(x => x.Especialidad)
                                                 .FirstOrDefaultAsync(x => x.MedicoId == id).ConfigureAwait(false);
           
            if (medico == null) { return BadRequest(); }

            var medicoResponse = _mapper.Map<MedicoResponse>(medico);

            return medicoResponse;
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> PostMedico([FromBody] MedicoRequest medicoRequest)
        {
            var medico = _mapper.Map<Medico>(medicoRequest);

            await _dbContext.Medicos.AddAsync(medico);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            var medicoResponse = _mapper.Map<MedicoResponse>(medico);

            return new CreatedAtRouteResult("getMedico", new {id = medicoResponse.MedicoId }
                                            ,medicoResponse);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> PutMedico(int id, [FromBody] MedicoRequest medicoRequest)
        {
            var medico = _mapper.Map<Medico>(medicoRequest);
            medico.MedicoId = id;

            _dbContext.Entry(medico).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return NoContent();

        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> DeleteMedico(int id)
        {
            var medico = await _dbContext.Medicos.Include(x => x.Citas)
                                         .FirstOrDefaultAsync(x => x.MedicoId == id).ConfigureAwait(false);

            _dbContext.Medicos.Remove(medico);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            var medicoResponse = _mapper.Map<MedicoResponse>(medico);

            return Ok(medicoResponse);

        }
    }
}
