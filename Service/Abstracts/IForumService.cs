using Core.Utilities.Results;
using Dto.ForumComments;
using Dto.ForumPosts;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAsyncResult = Core.Utilities.Results.IAsyncResult;

namespace Service.Abstracts
{
    public interface IForumService:IDbOperationEvent<ForumPost,Guid>,IDbOperationEvent<ForumComment,Guid>,IDbOperationEvent<ForumPostMatchSubmitUser,Guid>, IDbOperationEvent<UserProfession,Guid>
    {
        Task<IAsyncDataResult<List<GetForumPostDto>>> GetAllPostsAsync();
        Task<IAsyncDataResult<GetForumPostDto>> GetPostByIdAsync(Guid postId);
        Task<IAsyncResult> CreatePostAsync(CreateForumPostDto postDto);
        Task<IAsyncResult> UpdatePostAsync(UpdateForumPostDto postDto);
        Task<IAsyncResult> SoftDeletePostAsync(Guid postId);

        Task<IAsyncDataResult<List<GetForumCommentDto>>> GetAllCommentsAsync();
        Task<IAsyncDataResult<GetForumCommentDto>> GetCommentByIdAsync(Guid commentId);
        Task<IAsyncDataResult<List<GetForumCommentDto>>> GetCommentsByPostIdAsync(Guid postId);
        Task<IAsyncDataResult<List<GetForumCommentDto>>> GetChildCommentsByMainId(Guid commentId);
        Task<IAsyncResult> CreateCommentAsync(CreateForumCommentDto commentDto);
        Task<IAsyncResult> UpdateCommentAsync(UpdateForumCommentDto commentDto);
        Task<IAsyncResult> SoftDeleteCommentAsync(Guid commentId);
        Task<IAsyncResult> SubmitForForumPostMatchAsync(SubmitForumMatchDto submitDto);
        Task<IAsyncResult> MatchAllUsersForAllForumPostsAsync();
    }
}
