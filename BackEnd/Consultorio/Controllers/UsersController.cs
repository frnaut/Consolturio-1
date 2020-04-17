using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Consultorio.DataAccess;
using Consultorio.Models;
using Consultorio.Request;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Consultorio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this._dbContext = dbContext;
            this._userManager = userManager;
        }

        [HttpPost("Asignar-rol")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> AsignarRol(EditarRolRequest editarRol)
        {
            var usuario = await _userManager.FindByIdAsync(editarRol.UsuarioId).ConfigureAwait(false);
            
            if (usuario == null) { return NotFound(); }

            await _userManager.AddClaimAsync(usuario, new Claim(ClaimTypes.Role, editarRol.RolName))
                                            .ConfigureAwait(false);

            await _userManager.AddToRoleAsync(usuario, editarRol.RolName).ConfigureAwait(false);

            return Ok();
        }

        [HttpPost("Remover-rol")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> RemoverRol(EditarRolRequest editarRol)
        {
            var usuario = await _userManager.FindByIdAsync(editarRol.UsuarioId).ConfigureAwait(false);

            if (usuario == null) { return NotFound(); }

            await _userManager.RemoveClaimAsync(usuario, new Claim(ClaimTypes.Role, editarRol.RolName))
                                            .ConfigureAwait(false);

            await _userManager.RemoveFromRoleAsync(usuario, editarRol.RolName).ConfigureAwait(false);

            return Ok();
        }
    }
}