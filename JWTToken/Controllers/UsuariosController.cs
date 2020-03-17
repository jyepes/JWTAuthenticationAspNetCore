using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JWTToken.Contexts;
using JWTToken.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JWTToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public UsuariosController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpPost("AsignarUsuarioRol")]
        public async Task<ActionResult> AsignarRolUsuario(EditarRolDto model)
        {
            var usuario = await userManager.FindByIdAsync(model.UserId);
            if (usuario == null)
            {
                return BadRequest();
            }
            await userManager.AddClaimAsync(usuario, new Claim(ClaimTypes.Role, model.RolName));
            await userManager.AddToRoleAsync(usuario, model.RolName);

            return Ok();
        }


        [HttpPost("RemoverUsuarioRol")]
        public async Task<ActionResult> RemoverRolUsuario(EditarRolDto model)
        {
            var usuario = await userManager.FindByIdAsync(model.UserId);
            if (usuario == null)
            {
                return BadRequest();
            }
            await userManager.RemoveClaimAsync(usuario, new Claim(ClaimTypes.Role, model.RolName));
            await userManager.RemoveFromRoleAsync(usuario, model.RolName);

            return Ok();
        }
    }
}