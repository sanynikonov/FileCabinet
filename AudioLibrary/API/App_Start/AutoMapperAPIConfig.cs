using AutoMapper;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API
{
    public class AutoMapperAPIConfig : Profile
    {
        public AutoMapperAPIConfig()
        {
            CreateMap<UserDTO, UserModel>();
            CreateMap<UserModel, UserDTO>();

            CreateMap<CommentaryDTO, CommentaryModel>();
            CreateMap<CommentaryModel, CommentaryDTO>();

            CreateMap<SongDTO, SongModel>();
            CreateMap<SongModel, SongDTO>();

            CreateMap<SongsContainerDTO, SongsContainerModel>();
            CreateMap<SongsContainerModel, SongsContainerDTO>();

            CreateMap<RegistrationModel, UserDTO>();
        }
    }
}