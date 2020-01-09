using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using BLL;
using Microsoft.Owin.Security;
using AutoMapper;

namespace API.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private IUserService service;
        private IMapper mapper;

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return Request.GetOwinContext().Authentication;
            }
        }

        public AuthController(IUserService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("log-in")]
        public async Task<IHttpActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest($"{nameof(loginModel)} must be passed");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var claims = await service.AuthenticateUserAsync(loginModel.UserName, loginModel.Password, DefaultAuthenticationTypes.ApplicationCookie);
            
            if (!claims.IsAuthenticated)
            {
                return BadRequest("Wrong username or password");
            }

            AuthenticationManager.SignIn(claims);
            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("log-out")]
        public IHttpActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return Ok();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IHttpActionResult> Register([FromBody] RegistrationModel registrationModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Wrong login or password");

            try
            {
                var user = mapper.Map<UserDTO>(registrationModel);
                await service.AddUserAsync(user, registrationModel.Password);
                return Ok("Registered succesfully");
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost]
        [Route("exists")]
        public async Task<IHttpActionResult> Exists(UserModel model)
        {
            if (model.Email == null || model.PhoneNumber == null)
            {
                return BadRequest("Invalid email or password");
            }

            try
            {
                var result = await service.ExistsAsync(model.Email, model.PhoneNumber);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Authorize]
        [Route("change-password")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordModel model)
        {
            try
            {
                var userId = await service.GetUserIdByUserName(User.Identity.Name);
                await service.ChangePasswordAsync(userId, model.OldPassword, model.Password);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("username-taken/{userName}")]
        public async Task<IHttpActionResult> UserNameAlreadyTakenAsync(string userName)
        {
            try
            {
                var result = await service.UserNameAlreadyTakenAsync(userName);
                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
