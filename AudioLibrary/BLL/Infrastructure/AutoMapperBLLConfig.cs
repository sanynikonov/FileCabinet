using AutoMapper;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AutoMapperBLLConfig : Profile
    {
        public AutoMapperBLLConfig()
        {
            CreateMap<Commentary, CommentaryDTO>()
                .ForMember(p => p.SongId, x => x.MapFrom(c => c.Song.Id))
                .ForMember(p => p.User, x => x.Ignore());
            CreateMap<CommentaryDTO, Commentary>()
                .ForMember(p => p.User, x => x.Ignore());

            CreateMap<Song, SongDTO>()
                .ForMember(p => p.AlbumId, x => x.MapFrom(s => s.Album.Id))
                .ForMember(p => p.LikesAmount, x => x.MapFrom(s => s.Likes.Count));
            CreateMap<SongDTO, Song>();

            CreateMap<SongsContainer, SongsContainerDTO>()
                .ForMember(p => p.LikesAmount, x => x.MapFrom(s => s.Likes.Count))
                .ForMember(p => p.Author, x => x.Ignore());
            CreateMap<SongsContainerDTO, SongsContainer>()
                .ForMember(p => p.Author, x => x.Ignore());

            CreateMap<User, UserDTO>()
                .ForMember(p => p.Country, x => x.MapFrom(u => u.Country.Name));
            CreateMap<UserDTO, User>()
                .ForMember(p => p.Country, x => x.Ignore());
        }
    }
}
