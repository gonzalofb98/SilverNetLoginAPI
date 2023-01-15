using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dto.Request;
using System.Linq;
using Dto.Response;
using Services;
using Configuration;

namespace SilverNetLoginAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        ITokenHandlerService _service;
        public AuthController(UserManager<IdentityUser> userManager, ITokenHandlerService service)
        {
            _userManager = userManager;
            _service = service;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequestDTO user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);
                if (existingUser != null)
                {
                    return BadRequest("El correo electronico indicado ya existe!");
                }

                var isCreated = await _userManager.CreateAsync(new IdentityUser() { Email = user.Email, UserName = user.Email }, user.Password);
                if (isCreated.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(isCreated.Errors.Select(x => x.Description).ToList());
                }

            }
            else
            {
                return BadRequest("Se produjo algun error al registrar el usuario");
            }
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(UserLoginRequestDTO user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);
                if (existingUser == null)
                {
                    return BadRequest(new UserLoginResponseDTO()
                    {
                        Login = false,
                        Errors = new List<String>()
                        {
                            "Usuario o contraseña incorrecto!"
                        }
                    });
                }

                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);

                if (isCorrect)
                {
                    var pars = new TokenParameters()
                    {
                        Id = existingUser.Id,
                        PasswordHash = existingUser.PasswordHash,
                        UserName = existingUser.UserName

                    };

                    var jwtToken = _service.GenerateJwtToken(pars);

                    return Ok(new UserLoginResponseDTO()
                    {
                        Login = true,
                        Token = jwtToken
                    });

                }
                else
                {
                    return BadRequest(new UserLoginResponseDTO()
                    {
                        Login = false,
                        Errors = new List<String>()
                        {
                            "Usuario o contraseña incorrecto!"
                        }
                    });
                }

            }
            else
            {
                return BadRequest(new UserLoginResponseDTO()
                {
                    Login = false,
                    Errors = new List<String>()
                        {
                            "Usuario o contraseña incorrecto!"
                        }
                });
            }
        }
    }
}
