using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Consultorio.DataAccess;
using Consultorio.Models;
using Consultorio.Request;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Consultorio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EspecialidadesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public EspecialidadesController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<EspecialidadResponse>>> GetEspecialidades()
        {
            var especialidades = await _dbContext.Especialidades.ToListAsync()
                                                                .ConfigureAwait(false);

            var especialidadesResponse = _mapper.Map<List<EspecialidadResponse>>(especialidades);


            return especialidadesResponse;
        }

        [HttpGet("{id}", Name = "getEspecialidad")]
        public async Task<ActionResult<EspecialidadResponse>> GetEspecialidad(int id)
        {
            var especialidad = await _dbContext.Especialidades
                                    .FirstOrDefaultAsync(x => x.EspecialidadId == id).ConfigureAwait(false);
            
            if (especialidad == null) { return NotFound(); }

            var especialidadResponse = _mapper.Map<EspecialidadResponse>(especialidad);
            return especialidadResponse;
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> PostEspecialidad([FromBody] EspecialidadRequest especialidadRequest)
        {

            var especialidad = _mapper.Map<Especialidad>(especialidadRequest);
            await _dbContext.Especialidades.AddAsync(especialidad);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            var especialidadResponse = _mapper.Map<EspecialidadResponse>(especialidad);

            return new CreatedAtRouteResult("getEspecialidad", new {id = especialidadResponse.EspecialidadId  }
                                                                    , especialidadResponse);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> PutEspecialidad(int id, [FromBody] EspecialidadRequest especialidadRequest)
        {

            var especialidad = _mapper.Map<Especialidad>(especialidadRequest);
            especialidad.EspecialidadId = id;

            _dbContext.Entry(especialidad).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return NoContent();

        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<EspecialidadResponse>> DeleteEspecialidad(int id)
        {
            var especialidad = await _dbContext.Especialidades
                                               .FirstOrDefaultAsync(x => x.EspecialidadId == id).ConfigureAwait(false);

            if (especialidad == null) { return BadRequest(); }

            _dbContext.Especialidades.Remove(especialidad);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            var especialidadResponse = _mapper.Map<EspecialidadResponse>(especialidad);

            return Ok(especialidadResponse);
        }

    }
}
