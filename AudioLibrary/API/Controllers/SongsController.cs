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
    public class SongsController : ApiController
    {
        private ISongService service;
        private IMapper mapper;
        private IUserIdService userIdFinder;

        public SongsController(ISongService service, IMapper mapper, IUserIdService userIdFinder)
        {
            this.service = service;
            this.mapper = mapper;
            this.userIdFinder = userIdFinder;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Add([FromBody] SongModel songModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad song model format");
            }

            try
            {
                var song = mapper.Map<SongDTO>(songModel);
                await service.AddSongAsync(song);
                return Ok("Song was succesfully added");
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost]
        [Route("api/songs/uploadaudio")]
        public IHttpActionResult AddAudio()
        {
            try
            {
                var file = HttpContext.Current.Request.Files[0];
                var fileName = file.FileName;

                if (!fileName.EndsWith(".mp3"))
                {
                    return BadRequest("Only .mp3 allowed");
                }

                var path = HttpContext.Current.Server.MapPath("~/UsersContent/Audio");
                fileName = DateTime.Now.Ticks + ".mp3";
                var fullpath = Path.Combine(path, fileName);

                file.SaveAs(fullpath);

                return Ok(fileName);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Remove(int id)
        {
            try
            {
                await service.RemoveSongAsync(id);
                return Ok("Song was succesfully removed");
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

        [HttpGet]
        [Route("api/songs/{id}/audio")]
        public async Task<HttpResponseMessage> GetAudio(int id)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);

            var userId = await userIdFinder.GetUserIdByUserName(User.Identity.Name);
            var song = await service.GetSongAsync(id, userId);
            var fileName = song.AudioPath;
            //var fileName = "cover #0.jpg";

            var path = HttpContext.Current.Server.MapPath("~/UsersContent/Audio");
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
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var userId = await userIdFinder.GetUserIdByUserName(User.Identity.Name);
                var song = await service.GetSongAsync(id, userId);

                var songModel = mapper.Map<SongDTO>(song);

                return Ok(songModel);
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

        [HttpGet]
        [Route("api/songs/byname")]
        public async Task<IHttpActionResult> GetSongsByName([FromBody]string name)
        {
            try
            {
                var userId = await userIdFinder.GetUserIdByUserName(User.Identity.Name);
                var songs = await service.GetSongsByNameAsync(name, userId);

                var songModels = mapper.Map<IEnumerable<SongDTO>>(songs);

                return Ok(songModels);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        [Route("api/songscontainers/{id}/songs")]
        public async Task<IHttpActionResult> GetSongsByAlbum(int id)
        {
            try
            {
                var userId = await userIdFinder.GetUserIdByUserName(User.Identity.Name);
                var songs = await service.GetSongsByAlbumIdAsync(id, userId);

                var songModels = mapper.Map<IEnumerable<SongDTO>>(songs);

                return Ok(songModels);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        [Route("api/songs/liked")]
        public async Task<IHttpActionResult> GetLikedSongs()
        {
            try
            {
                var userId = await userIdFinder.GetUserIdByUserName(User.Identity.Name);
                var songs = await service.GetLikedSongsAsync(userId);

                var songModels = mapper.Map<IEnumerable<SongDTO>>(songs);

                return Ok(songModels);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPut]
        [Route("api/songs/{id}/like")]
        public async Task<IHttpActionResult> PutLike(int id)
        {
            try
            {
                var userId = await userIdFinder.GetUserIdByUserName(User.Identity.Name);
                await service.SlapLikeAsync(id, userId);

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPut]
        [Route("api/songs/{id}/listen")]
        public async Task<IHttpActionResult> AddListen(int id)
        {
            try
            {
                await service.AddListenAsync(id);

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        protected override void Dispose(bool disposing)
        {
            service.Dispose();
            userIdFinder.Dispose();
            base.Dispose(disposing);
        }
    }
}
