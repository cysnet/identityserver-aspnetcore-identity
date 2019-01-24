using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using web2.Data;

namespace web2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;


        private readonly SignInManager<web2User> _signInManager;
        private readonly UserManager<web2User> _userManager;
        private readonly ITokenCreationService _tokenServices;
        public ValuesController(UserManager<web2User> userManager,
            SignInManager<web2User> signInManager,
             ITokenCreationService tokenServices,
              RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenServices = tokenServices;
            _roleManager = roleManager;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }


        // POST api/values
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] UserRegist Input)
        {
            //var user = new web2User { UserName = Input.Email, Email = Input.Email };
            //await _signInManager.SignInAsync(user, isPersistent: false);
            var user =await _userManager.FindByNameAsync(Input.Email);
            var claims = new List<Claim>
            {
                new Claim("client_id", "cobbler.gateway.password.id.20181219"),
                new Claim("sub", "aaa"),
                new Claim("userid","123"),
                new Claim(JwtClaimTypes.Role,"admin1"),
                new Claim(JwtClaimTypes.Role,"admin2")
            };
            var token = new Token
            {
                //CreationTime = DateTime.UtcNow,
                Audiences = { "cobbler.gateway" },
                Issuer = "http://localhost:58062",
                Lifetime = 36000,
                Claims = claims,
                ClientId = "cobbler.gateway.password.id.20181219",
                AccessTokenType = IdentityServer4.Models.AccessTokenType.Jwt
            };
            var tokenStr =await  _tokenServices.CreateTokenAsync(token);
            return Ok(tokenStr);



            //var user = new web2User { UserName = Input.Email, Email = Input.Email };
            //var result = await _userManager.CreateAsync(user, Input.Password);
            //if (result.Succeeded)
            //{
            //    await _signInManager.SignInAsync(user, isPersistent: false);
            //    var claims = new List<Claim>
            //{
            //    new Claim("client_id", "cobbler.gateway.password.id.20181219"),
            //    new Claim("sub", "aaa")
            //};
            //    var token = new Token
            //    {
            //        CreationTime = DateTime.UtcNow,
            //        Audiences = { "https://idsvr.com/resources" },
            //        Issuer = "https://idsvr.com",
            //        Lifetime = 36000,
            //        Claims = claims,
            //        ClientId = "cobbler.gateway.password.id.20181219",
            //        AccessTokenType = IdentityServer4.Models.AccessTokenType.Jwt
            //    };
            //    _tokenServices.CreateTokenAsync(token);
            //    return Ok("Succeeded");
            //}
            //return Ok("Fail");
        }
        [HttpPost]
        [Route("logout")]
        public async Task<ActionResult> logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("OK");
        }

        [HttpPost]
        [Route("addrole")]
        public async Task<ActionResult> addrole([FromQuery] string role)
        {
            var IR = await _roleManager.CreateAsync(new IdentityRole(role));
            //await _signInManager.SignOutAsync();
            return Ok("OK");
        }

        [HttpPost]
        [Route("attachrole")]
        public async Task<ActionResult> attachrole([FromQuery] string role, [FromQuery] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var IR = await _userManager.AddToRoleAsync(user, role);
            //await _signInManager.SignOutAsync();
            return Ok("OK");
        }
    }
}
