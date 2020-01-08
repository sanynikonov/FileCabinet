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
    //[RoutePrefix("songs-containers")]
    public class SongsContainersController : ApiController
    {
        private ISongsContainerService service;
        private IUserIdService userIdFinder;
        private IMapper mapper;

        public SongsContainersController(ISongsContainerService service, IUserIdService userIdService, IMapper mapper)
        {
            this.service = service;
            userIdFinder = userIdService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var userId = await userIdFinder.GetUserIdByUserName(User.Identity.Name);
                var container = await service.GetSongsContainerAsync(id, userId);
                return Ok(container);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] SongsContainerModel songsContainer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var containerDTO = mapper.Map<SongsContainerDTO>(songsContainer);
                containerDTO.Author = mapper.Map<UserDTO>(songsContainer.Author);
                await service.AddSongsContainerAsync(containerDTO);
                return Ok("Songs container was added");
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost]
        [Route("api/songscontainers/cover")]
        public IHttpActionResult PostCover()
        {
            try
            {
                var file = HttpContext.Current.Request.Files[0];
                var fileName = file.FileName;

                if (!(fileName.EndsWith(".jpg") || fileName.EndsWith(".png")))
                {
                    return BadRequest("Only .jpg and .png allowed");
                }

                var path = HttpContext.Current.Server.MapPath("~/UsersContent/Covers");
                var count = new DirectoryInfo(path).GetFiles().Count();
                fileName = "cover #" + count + fileName.Substring(fileName.Length - 4);
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
        [Route("api/songscontainers/{id}/cover")]
        public async Task<HttpResponseMessage> GetCover(int id)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);

            var userId = await userIdFinder.GetUserIdByUserName(User.Identity.Name);
            var conatiner = await service.GetSongsContainerAsync(id, userId);
            var fileName = conatiner.CoverPath;
            //var fileName = "cover #0.jpg";

            var path = HttpContext.Current.Server.MapPath("~/UsersContent/Covers");
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
        [Route("api/songscontainers/bygenre")]
        public async Task<IHttpActionResult> GetByGenre([FromBody]string genre)
        {
            try
            {
                var userId = await userIdFinder.GetUserIdByUserName(User.Identity.Name);
                var containers = await service.GetSongsContainersByGenreAsync(genre, userId);
                var containersModels = containers.Select(x =>
                {
                    var conMod = mapper.Map<SongsContainerModel>(x);
                    conMod.Author = mapper.Map<UserModel>(x.Author);
                    return conMod;
                });

                return Ok(containersModels);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        [Route("api/songscontainers/byname")]
        public async Task<IHttpActionResult> GetByName([FromBody]string name)
        {
            try
            {
                var userId = await userIdFinder.GetUserIdByUserName(User.Identity.Name);
                var containers = await service.GetSongsContainersByNameAsync(name, userId);
                var containersModels = containers.Select(x =>
                {
                    var conMod = mapper.Map<SongsContainerModel>(x);
                    conMod.Author = mapper.Map<UserModel>(x.Author);
                    return conMod;
                });

                return Ok(containersModels);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        [Route("api/authors/{id}/albums/")]
        public async Task<IHttpActionResult> GetAlbumsByAuthor(string authorId)
        {
            try
            {
                var userId = await userIdFinder.GetUserIdByUserName(User.Identity.Name);
                var containers = await service.GetAlbumsByAuthorAsync(authorId, userId);
                var containersModels = containers.Select(x =>
                {
                    var conMod = mapper.Map<SongsContainerModel>(x);
                    conMod.Author = mapper.Map<UserModel>(x.Author);
                    return conMod;
                });

                return Ok(containersModels);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        [Route("api/user/playlists/")]
        public async Task<IHttpActionResult> GetUserPlaylists()
        {
            try
            {
                var userId = await userIdFinder.GetUserIdByUserName(User.Identity.Name);
                var containers = await service.GetUserPlaylistsAsync(userId);
                var containersModels = containers.Select(x =>
                {
                    var conMod = mapper.Map<SongsContainerModel>(x);
                    conMod.Author = mapper.Map<UserModel>(x.Author);
                    return conMod;
                });

                return Ok(containersModels);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        [Route("api/user/albums/")]
        public async Task<IHttpActionResult> GetUserAlbums()
        {
            try
            {
                var userId = await userIdFinder.GetUserIdByUserName(User.Identity.Name);
                var containers = await service.GetUserAlbumsAsync(userId);
                var containersModels = containers.Select(x =>
                {
                    var conMod = mapper.Map<SongsContainerModel>(x);
                    conMod.Author = mapper.Map<UserModel>(x.Author);
                    return conMod;
                });

                return Ok(containersModels);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        [Route("api/user/followings/playlists")]
        public async Task<IHttpActionResult> GetPlaylistsByFollowings()
        {
            try
            {
                var userId = await userIdFinder.GetUserIdByUserName(User.Identity.Name);
                var containers = await service.GetPlaylistsByFollowingsAsync(userId);
                var containersModels = containers.Select(x =>
                {
                    var conMod = mapper.Map<SongsContainerModel>(x);
                    conMod.Author = mapper.Map<UserModel>(x.Author);
                    return conMod;
                });

                return Ok(containersModels);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        [Route("api/songscontainers/{id}/genres")]
        public async Task<IHttpActionResult> GetGenresBySongsContainer(int id)
        {
            try
            {
                var genres = await service.GetGenresBySongsContainerAsync(id);

                return Ok(genres);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPut]
        [Route("api/songscontainers/{id}/like")]
        public async Task<IHttpActionResult> PutLike(int id)
        {
            try
            {
                var userId = await userIdFinder.GetUserIdByUserName(User.Identity.Name);
                await service.SlapLikeAsync(id, userId);

                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPut]


        protected override void Dispose(bool disposing)
        {
            service.Dispose();
            userIdFinder.Dispose();
            base.Dispose(disposing);
        }
    }
}