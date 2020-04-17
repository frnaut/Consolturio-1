using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using Consultorio.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Web.Administration;

namespace Consultorio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly IConfiguration _configuration;

        public CuentasController(UserManager<ApplicationUser> userManager,
                                        SignInManager<ApplicationUser> signInManager )
        {
            
            this._userManager = userManager;
            this._signInManager = signInManager;
            //this._configuration = configuration;
        }

        [HttpPost("Crear")]
        public async Task<ActionResult<UserToken>> CreteUser([FromBody] UserInfo model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password).ConfigureAwait(false);
            
            if (result.Succeeded)
            {
                return BuildToken(model, new List<string>());
            }
            else
            {
                return BadRequest("Email or Password invalid");
            }


        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] UserInfo user)
        {
      

            var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password,
                                                                    isPersistent: true, lockoutOnFailure: false)
                                                                    .ConfigureAwait(false);

            if (result.Succeeded)
            {
                var usuario = await _userManager.FindByEmailAsync(user.Email).ConfigureAwait(false);

         
                var roles = await _userManager.GetRolesAsync(usuario).ConfigureAwait(false);


                if (result.Succeeded)
                {
                    return BuildToken(user, roles);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return BadRequest(ModelState);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return BadRequest(ModelState);
            }


        }


        #region BuildToken
        private UserToken BuildToken(UserInfo model, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, model.Email),
                new Claim("Consultorio", "Consultorio Medico"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach(var rol in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, rol));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sDFADAD1244@DADAFSKFSJKFddaldDMA58D52"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expirationn = DateTime.UtcNow.AddHours(1);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expirationn,
                signingCredentials: creds);

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expirationn
            };
        }

        #endregion
    }
}