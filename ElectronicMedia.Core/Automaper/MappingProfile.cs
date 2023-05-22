using AutoMapper;
using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core.Repository.Models;
using Konscious.Security.Cryptography;
using System.Text;

namespace ElectronicMedia.Core.Automaper
{
    public class Mapping : Profile
    {
        const int memorySize = 1024;
        const int iterations = 10;
        public Mapping()
        {
            #region User
            CreateMap<UserRegisterModel, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => EncodePassword(src.Password)))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => RoleType.UserNormal))
                .ForMember(dest => dest.IsActived, opt => opt.MapFrom(src => true));
            #endregion

            #region comments
            CreateMap<CommentModel, Comment>();
            CreateMap<ReplyCommentModel, ReplyComment>();
            #endregion

            #region posts
            CreateMap<PostModel, Post>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.PublishedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => PostStatusModel.Pending))
                .ForMember(dest => dest.SubCategoryId, opt => opt.MapFrom(src => src.SubCategoryId));
            CreateMap<PostCategoryModel, PostCategory>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId == null ? null : src.ParentId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.SubCategories, opt => opt.MapFrom(src => new List<PostCategory>()));
            CreateMap<PostDetailModel, PostDetail>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.AuthorId))
                .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.PostId))
                .ForMember(dest => dest.Liked, opt => opt.MapFrom(src => src.Liked));
            #endregion
        }
        #region private medthod
        private string EncodePassword(string password)
        {
            var salt = new byte[16];
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = 4,
                Iterations = iterations,
                MemorySize = memorySize
            };
            var hash = argon2.GetBytes(16);
            var saltPlusHash = new byte[16 + hash.Length];
            Buffer.BlockCopy(salt, 0, saltPlusHash, 0, salt.Length);
            Buffer.BlockCopy(hash, 0, saltPlusHash, salt.Length, hash.Length);
            Array.Clear(salt, 0, salt.Length);
            Array.Clear(argon2.GetBytes(memorySize), 0, argon2.GetBytes(memorySize).Length);
            return Convert.ToBase64String(saltPlusHash);
        }
        #endregion

    }
}
