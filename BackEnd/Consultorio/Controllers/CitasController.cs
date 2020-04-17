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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Consultorio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitasController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CitasController(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            this._dbContext = applicationDbContext;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<List<CitaResponse>> GetCitas()
        {
            var citas = await _dbContext.Citas.Include(x => x.Medico)
                                              .Include(x => x.Paciente).ToListAsync().ConfigureAwait(false);

            var citasResponse = _mapper.Map<List<CitaResponse>>(citas);

            return citasResponse;

        }

        [HttpGet("{id}", Name = "getCita")]
        public async Task<ActionResult<CitaResponse>> GetCita(int id)
        {
            var cita = await _dbContext.Citas.Include(x => x.Medico).Include(x => x.Paciente)
                                             .FirstOrDefaultAsync(x => x.CitaId == id).ConfigureAwait(false);

            var citaResponse = _mapper.Map<CitaResponse>(cita);

            return citaResponse;

        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> PostCita([FromBody] CitaRequest citaRequest)
        {
            var cita = _mapper.Map<Cita>(citaRequest);

            await _dbContext.Citas.AddAsync(cita);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            var citaResponse = _mapper.Map<CitaResponse>(cita);

            return new CreatedAtRouteResult("getCita", new { id = cita.CitaId }, citaResponse);

        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<CitaResponse>> PutCita(int id, [FromBody] CitaRequest citaRequest)
        {
            var cita = _mapper.Map<Cita>(citaRequest);
            cita.CitaId = id;

            _dbContext.Entry(cita).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> DeleteCita(int id)
        {
            var cita = await _dbContext.Citas.Include(x => x.Paciente).Include(x => x.Medico)
                                             .FirstOrDefaultAsync(x => x.CitaId == id).ConfigureAwait(false);
           
            if (cita == null) { return BadRequest(); }

            _dbContext.Remove(cita);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            var citaResponse = _mapper.Map<CitaResponse>(cita);

            return Ok(citaResponse);

        }


    }
}