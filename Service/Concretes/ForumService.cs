using AutoMapper;
using Core.Utilities.Results;
using Dto.ForumComments;
using Dto.ForumPosts;
using Entity;
using Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Concretes
{
    public class ForumService : IForumService
    {
        public ITBaseService<ForumPost, Guid> ForumPostCurrent { get; }
        public ITBaseService<ForumComment, Guid> ForumCommentCurrent { get; }
        ITBaseService<ForumPost, Guid> IDbOperationEvent<ForumPost, Guid>.Current => ForumPostCurrent;
        ITBaseService<ForumComment, Guid> IDbOperationEvent<ForumComment, Guid>.Current => ForumCommentCurrent;
        private readonly IMapper _mapper;
        public ForumService(ITBaseService<ForumPost, Guid> forumPostService, ITBaseService<ForumComment, Guid> forumCommentService, IMapper mapper)
        {
            ForumPostCurrent = forumPostService;
            ForumCommentCurrent = forumCommentService;
            _mapper = mapper;
        }

        public async Task<Core.Utilities.Results.IAsyncResult> CreateCommentAsync(CreateForumCommentDto commentDto)
        {
            var commentMap = _mapper.Map<ForumComment>(commentDto);
            commentMap.CreatedDate = DateTime.UtcNow;
            var result = await ForumCommentCurrent.AddAsync(commentMap);
            return new AsyncSuccessResult();

        }

        public async Task<Core.Utilities.Results.IAsyncResult> CreatePostAsync(CreateForumPostDto postDto)
        {
            var postMap = _mapper.Map<ForumPost>(postDto);
            postMap.CreatedDate = DateTime.UtcNow;
            var result = await ForumPostCurrent.AddAsync(postMap);
            return new AsyncSuccessResult();
        }

        public async Task<IAsyncDataResult<List<GetForumCommentDto>>> GetAllCommentsAsync()
        {
            var result = await ForumCommentCurrent.GetAllListAsync();
            var commentDtos = _mapper.Map<List<GetForumCommentDto>>(result);
            return new AsyncSuccessDataResult<List<GetForumCommentDto>>(Task.FromResult(commentDtos));
        }

        public async Task<IAsyncDataResult<List<GetForumPostDto>>> GetAllPostsAsync()
        {
            var result = await ForumPostCurrent.GetAllListAsync();
            var postDtos = _mapper.Map<List<GetForumPostDto>>(result);
            return new AsyncSuccessDataResult<List<GetForumPostDto>>(Task.FromResult(postDtos));
        }

        public async Task<IAsyncDataResult<GetForumCommentDto>> GetCommentByIdAsync(Guid commentId)
        {
            var comment = await ForumCommentCurrent.FirstOrDefaultAsync(x => x.Id == commentId);
            if (comment == null)
            {
                return new AsyncErrorDataResult<GetForumCommentDto>(Task.FromResult("Comment not found"));
            }
            var commentDto = _mapper.Map<GetForumCommentDto>(comment);
            return new AsyncSuccessDataResult<GetForumCommentDto>(Task.FromResult(commentDto));
        }

        public async Task<IAsyncDataResult<GetForumPostDto>> GetPostByIdAsync(Guid postId)
        {
            var post = await ForumPostCurrent.FirstOrDefaultAsync(x => x.Id == postId);
            if (post == null)
            {
                return new AsyncErrorDataResult<GetForumPostDto>(Task.FromResult("Post not found"));
            }
            var postDto = _mapper.Map<GetForumPostDto>(post);
            return new AsyncSuccessDataResult<GetForumPostDto>(Task.FromResult(postDto));
        }

        public async Task<Core.Utilities.Results.IAsyncResult> SoftDeleteCommentAsync(Guid commentId)
        {
            var comment = await ForumCommentCurrent.FirstOrDefaultAsync(x => x.Id == commentId);
            if (comment == null)
            {
                return new AsyncErrorResult(Task.FromResult("Comment not found"));
            }
            comment.IsDeleted = true;
            comment.DeletedDate = DateTime.UtcNow;
            var result = await ForumCommentCurrent.UpdateAsync(comment);
            return new AsyncSuccessResult();
        }

        public async Task<Core.Utilities.Results.IAsyncResult> SoftDeletePostAsync(Guid postId)
        {
            var post = await ForumPostCurrent.FirstOrDefaultAsync(x => x.Id == postId);
            if (post == null)
            {
                return new AsyncErrorResult(Task.FromResult("Post not found"));
            }
            post.IsDeleted = true;
            post.DeletedDate = DateTime.UtcNow;
            var result = await ForumPostCurrent.UpdateAsync(post);
            return new AsyncSuccessResult();

        }

        public async Task<Core.Utilities.Results.IAsyncResult> UpdateCommentAsync(UpdateForumCommentDto commentDto)
        {
            var commentMap = _mapper.Map<ForumComment>(commentDto);
            commentMap.UpdatedDate = DateTime.UtcNow;
            var result = await ForumCommentCurrent.UpdateAsync(commentMap);
            return new AsyncSuccessResult();
        }

        public async Task<Core.Utilities.Results.IAsyncResult> UpdatePostAsync(UpdateForumPostDto postDto)
        {
            var postMap = _mapper.Map<ForumPost>(postDto);
            postMap.UpdatedDate = DateTime.UtcNow;
            var result = await ForumPostCurrent.UpdateAsync(postMap);
            return new AsyncSuccessResult();
        }

        public async Task<IAsyncDataResult<List<GetForumCommentDto>>> GetCommentsByPostIdAsync(Guid postId)
        {
            var comments = await ForumCommentCurrent.GetAllListAsync(x => x.ForumPostId == postId);
            var commentDtos = _mapper.Map<List<GetForumCommentDto>>(comments).ToList();
            return new AsyncSuccessDataResult<List<GetForumCommentDto>>(Task.FromResult(commentDtos));

        }        

        public Task<IAsyncDataResult<List<GetForumCommentDto>>> GetChildCommentsByMainId(Guid commentId)
        {
            throw new NotImplementedException();
        }

        public Task<IAsyncDataResult<List<GetForumCommentDto>>> GetChildCommentsByPostId(Guid postId)
        {
            throw new NotImplementedException();
        }
    }
}
