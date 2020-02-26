using AutoMapper;
using BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : ApiController
    {
        private IUserService service;
        private IMapper mapper;

        public UsersController(IUserService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("api/users/photo")]
        public IHttpActionResult PostPhoto()
        {
            try
            {
                var file = HttpContext.Current.Request.Files[0];
                var fileName = file.FileName;

                if (!(fileName.EndsWith(".jpg") || fileName.EndsWith(".png")))
                {
                    return BadRequest("Only .jpg and .png allowed");
                }

                var path = HttpContext.Current.Server.MapPath("~/UsersContent/Photos");

                fileName = DateTime.Now.Ticks + fileName.Substring(fileName.Length - 4);
                var fullpath = Path.Combine(path, fileName);

                file.SaveAs(fullpath);

                return Ok(fileName);

            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        [Route("api/users/{id}/photo")]
        public async Task<HttpResponseMessage> GetPhoto(string id)
        {
            var user = await service.GetUserByIdAsync(id);
            if (user.PhotoPath == null || user.PhotoPath == "")
                return new HttpResponseMessage(HttpStatusCode.NotFound);

            var result = new HttpResponseMessage(HttpStatusCode.OK);

            
            var fileName = user.PhotoPath;
            //var fileName = "cover #0.jpg";

            var path = HttpContext.Current.Server.MapPath("~/UsersContent/Photos");
            var fullpath = Path.Combine(path, fileName);

            var fileBytes = File.ReadAllBytes(fullpath);
            var memoryStream = new MemoryStream(fileBytes);
            result.Content = new StreamContent(memoryStream);

            var headers = result.Content.Headers;
            headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            headers.ContentDisposition.FileName = fileName;
            headers.ContentType =
                new MediaTypeHeaderValue("application/" + fileName.Substring(fileName.Length - 3));
            headers.ContentLength = memoryStream.Length;
            return result;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(string id)
        {
            try
            {
                var user = await service.GetUserByIdAsync(id);
                var userModel = mapper.Map<UserModel>(user);

                return Ok(userModel);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/users/by-phone/{phoneNumber}")]
        public async Task<IHttpActionResult> GetByPhoneNumber(string phoneNumber)
        {
            try
            {
                var user = await service.GetUserByPhoneNumberAsync(phoneNumber);
                var userModel = mapper.Map<UserModel>(user);

                return Ok(userModel);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/users/by-email/{email}")]
        public async Task<IHttpActionResult> GetByEmail(string email)
        {
            try
            {
                var user = await service.GetUserByEmailAsync(email);
                var userModel = mapper.Map<UserModel>(user);

                return Ok(userModel);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/users/by-username/{userName}")]
        public async Task<IHttpActionResult> GetByUserName(string userName)
        {
            try
            {
                var user = await service.GetUserByUserNameAsync(userName);
                var userModel = mapper.Map<UserModel>(user);

                return Ok(userModel);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/users/by-subname/{subname}")]
        public async Task<IHttpActionResult> GetUsersBySubname(string subname)
        {
            try
            {
                var users = await service.GetUsersBySubnameAsync(subname);
                var usersModels = mapper.Map<IEnumerable<UserModel>>(users);

                return Ok(usersModels);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/users/{id}/followers")]
        public async Task<IHttpActionResult> GetFollowers(string id)
        {
            try
            {
                var users = await service.GetFollowersAsync(id);
                var usersModels = mapper.Map<IEnumerable<UserModel>>(users);

                return Ok(usersModels);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/users/{id}/followings")]
        public async Task<IHttpActionResult> GetFollowings(string id)
        {
            try
            {
                var users = await service.GetFollowingsAsync(id);
                var usersModels = mapper.Map<IEnumerable<UserModel>>(users);

                return Ok(usersModels);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("api/users/{id}/followings/{accountId}")]
        public async Task<IHttpActionResult> FollowToAccount(string id, string accountId)
        {
            try
            {
                await service.FollowToAccountAsync(accountId, id);

                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Senior admin")]
        [Route("api/add-admin/{id}")]
        public async Task<IHttpActionResult> AddAdmin(string id)
        {
            try
            {
                await service.AddToAdminsAsync(id);

                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Senior admin")]
        [Route("api/add-artist/{id}")]
        public async Task<IHttpActionResult> AddArstist(string id)
        {
            try
            {
                await service.AddToArtistsAsync(id);

                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Senior admin")]
        [Route("api/delete-admin/{id}")]
        public async Task<IHttpActionResult> DeleteAdmin(string id)
        {
            try
            {
                await service.DeleteFromAdminsAsync(id);

                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Senior admin")]
        [Route("api/delete-artist/{id}")]
        public async Task<IHttpActionResult> DeleteArtist(string id)
        {
            try
            {
                await service.DeleteFromArtistsAsync(id);

                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
