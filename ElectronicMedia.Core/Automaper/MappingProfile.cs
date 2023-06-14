/*********************************************************************
 * 
 * PROPRIETARY and CONFIDENTIAL
 * 
 * This is licensed from, and is trade secret of:
 * 
 *          Group 10 - PRN231 - SU23
 *          FPT University, Education and Training zone
 *          Hoa Lac Hi-tech Park, Km29, Thang Long Highway
 *          Ha Noi, Viet Nam
 *          
 * Refer to your License Agreement for restrictions on use,
 * duplication, or disclosure
 * 
 * RESTRICTED RIGHTS LEGEND
 * 
 * Use, duplication or disclosure is the
 * subject to restriction in Articles 736 and 738 of the 
 * 2005 Civil Code, the Intellectual Property Law and Decree 
 * No. 85/2011/ND-CP amending and supplementing a number of 
 * articles of Decree 100/ND-CP/2006 of the Government of Viet Nam
 * 
 * 
 * Copy right 2023 © PRN231 - SU23 - Group 10 ®. All Rights Reserved
 * 
 * Unpublished - All rights reserved under the copyright laws 
 * of the Government of Viet Nam
*********************************************************************/

using AutoMapper;
using ElectronicMedia.Core.Common.Extension;
using ElectronicMedia.Core.Repository.Entity;
using ElectronicMedia.Core.Repository.Models;
using Konscious.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.Text;

namespace ElectronicMedia.Core.Automaper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            #region User
            CreateMap<UserRegisterModel, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => CommonService.EncodePassword(src.Password)))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => RoleType.UserNormal))
                .ForMember(dest => dest.IsActived, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => CommonService.InitAvatarUser()));
            CreateMap<User, UserProfileModel>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => "data:image/jpg;base64," + CommonFunct.Decode(src.Image)));
            #endregion

            #region comments
            CreateMap<CommentModel, Comment>();
            CreateMap<ReplyCommentModel, ReplyComment>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId)).ReverseMap();
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
                .ForMember(dest => dest.SubCategoryId, opt => opt.MapFrom(src => src.SubCategoryId))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => (src.FileURL != null)? CommonService.ConvertFileToURL(src.FileURL) : CommonService.InitAvatarUser()));
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
            CreateMap<Post, PostViewModel>()
                .ForMember(dest => dest.Image, otp => otp.MapFrom(src => "data:image/jpeg;base64," + CommonFunct.Decode(src.Image)));
            #endregion
        }
    }
}
