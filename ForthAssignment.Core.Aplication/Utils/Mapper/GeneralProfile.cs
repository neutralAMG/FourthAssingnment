using AutoMapper;
using ForthAssignment.Core.Aplication.Models.Comment;
using ForthAssignment.Core.Aplication.Models.Post;
using ForthAssignment.Core.Aplication.Models.User;
using ForthAssignment.Core.Aplication.Models.UserFriend;
using ForthAssignment.Core.Domain.Entities;

namespace ForthAssignment.Core.Aplication.Utils.Mapper
{
	public class GeneralProfile : Profile
	{
		public GeneralProfile()
		{
			#region User model configuration
			CreateMap<User, UserModel>()
				.ForMember(dest => dest.UserFriends, opt => opt.MapFrom(src => src.UserFriends))
				.ForMember(dest => dest.UserPosts, opt => opt.MapFrom(src => src.UserPosts))
				.ForMember(dest => dest.UserComments, opt => opt.MapFrom(src => src.UserComments))
				.ReverseMap();

			CreateMap<User, UserSaveModel>()
                 .ForMember(dest => dest.File, opt => opt.Ignore())
                 .ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore())
                .ReverseMap()
				.ForMember(dest => dest.UserFriends, opt => opt.Ignore())
				.ForMember(dest => dest.UserPosts, opt => opt.Ignore())
				.ForMember(dest => dest.UserComments, opt => opt.Ignore());

			#endregion

			#region UserFriends model configuration
			CreateMap<UserFriend, UserFriendModel>()
				.ForMember(dest => dest.UsersFriend, opt => opt.MapFrom(src => src.UsersFriend))
				.ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
				.ReverseMap()
				.ForMember(dest => dest.UserId, opt => opt.Ignore())
				.ForMember(dest => dest.UserId, opt => opt.Ignore())
				.ForMember(dest => dest.UserFriendId, opt => opt.Ignore());

			CreateMap<UserFriend, UserFriendSaveModel>()
				.ReverseMap()
				.ForMember(dest => dest.UsersFriend, opt => opt.Ignore())
				.ForMember(dest => dest.User, opt => opt.Ignore());

			#endregion

			#region Post model configuration
			CreateMap<Post, PostModel>()
				.ForMember(dest => dest.UserThatPostThis, opt => opt.MapFrom(src => src.UserThatPostThis))
				.ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
				.ReverseMap()
				.ForMember(dest => dest.Comments, opt => opt.Ignore())
				.ForMember(dest => dest.UserId, opt => opt.Ignore())
				.ForMember(dest => dest.UserThatPostThis, opt => opt.Ignore());

			CreateMap<Post, PostSaveModel>()
                .ForMember(dest => dest.File, opt => opt.Ignore())
                .ReverseMap()
				.ForMember(dest => dest.UserThatPostThis, opt => opt.Ignore())
				.ForMember(dest => dest.Comments, opt => opt.Ignore());


			#endregion

			#region Comment model configuration
			CreateMap<Comment, CommentModel>()
				.ForMember(dest => dest.UserThatCommentetThis, opt => opt.MapFrom(src => src.UserThatCommentetThis))
				.ReverseMap()
				.ForMember(dest => dest.UserId, opt => opt.Ignore());

			CreateMap<Comment, CommentSaveModel>()
                .ForMember(dest => dest.File, opt => opt.Ignore())
                .ReverseMap()
				.ForMember(dest => dest.UserThatCommentetThis, opt => opt.Ignore());

			#endregion

		}
	}
}
