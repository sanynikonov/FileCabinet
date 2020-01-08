using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using BLL;

namespace API.Controllers
{
    [Authorize]
    public class CommentariesController : ApiController
    {
        private ICommentaryService service;
        private IMapper mapper;

        public CommentariesController(ICommentaryService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetCommentary(int id)
        {
            try
            {
                var commentary = await service.GetCommentaryAsync(id);
                var comMod = mapper.Map<CommentaryModel>(commentary);
                comMod.User = mapper.Map<UserModel>(commentary.User);
                return Ok(comMod);
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
        [Route("api/songs/{id}/commentaries")]
        public async Task<IHttpActionResult> GetCommentariesBySongId(int id)
        {
            try
            {
                var commentaries = await service.GetCommentariesBySongIdAsync(id);

                var commentariesModels = commentaries.Select(x =>
                {
                    var comMod = mapper.Map<CommentaryModel>(x);
                    comMod.User = mapper.Map<UserModel>(x.User);
                    return comMod;
                });
                return Ok(commentariesModels);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> AddCommentary([FromBody] CommentaryModel commentaryModel)
        {
            try
            {
                var commentaryDTO = mapper.Map<CommentaryDTO>(commentaryModel);
                commentaryDTO.User = mapper.Map<UserDTO>(commentaryModel.User);
                await service.AddCommentaryAsync(commentaryDTO);
                return Ok("Commentary was added succesfully");
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
                await service.RemoveCommentaryAsync(id);
                return Ok("Commentary was deleted succesfully");
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

        protected override void Dispose(bool disposing)
        {
            service.Dispose();
            base.Dispose(disposing);
        }
    }
}
